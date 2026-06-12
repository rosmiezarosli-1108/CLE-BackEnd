using CLE_BackEnd.Data;
using CLE_BackEnd.Models;
using CLE_BackEnd.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace CLE_BackEnd.BackgroundWorkers;

public class AleAutomationBackgroundWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AleAutomationBackgroundWorker> _logger;

    public AleAutomationBackgroundWorker(IServiceProvider serviceProvider,
        ILogger<AleAutomationBackgroundWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("ALE Automation Background Worker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var notifService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                    
                    await ProcessAutoAcceptAsync(dbContext, notifService);
                    await ProcessAutoRejectAsync(dbContext, notifService);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ALE Automation Background Worker failed.");
            }
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task ProcessAutoAcceptAsync(ApplicationDbContext dbContext, INotificationService notifService)
    {
        var now = DateTime.UtcNow;
        var todayDate = DateOnly.FromDateTime(DateTime.Today);
        var currentTime = TimeOnly.FromDateTime(DateTime.Now);

        var pendingAssignments = await dbContext.AleAssignedHauliers
            .Include(a => a.AleContainer)
            .Include(a => a.AleTimeSlot)
            .Include(a => a.AleTimeSlot.Terminal)
            .Where(a => a.AleContainer != null
                        && (a.AleContainer.Status == "Enroute")
                        && a.AleTimeSlot != null
                        && a.AleTimeSlot.Date == todayDate
                        && !a.AleTimeSlot.IsCancelled)
            .ToListAsync();

        foreach (var assignment in pendingAssignments)
        {
            var container = assignment.AleContainer;
            var slot = assignment.AleTimeSlot;

            var scheduleRule = await dbContext.AleTerminalSchedules
                .FirstOrDefaultAsync(s => s.TerminalId == slot.TerminalId);
            if (scheduleRule == null || scheduleRule.AutoAcceptMinutes <= 0)
                continue;

            var timeParts = slot.Time.Split('-');
            if (timeParts.Length > 0 && TimeOnly.TryParse(timeParts[0].Trim().Trim(':'), out var slotStartTime))
            {
                var timeUntilSlot = slotStartTime - currentTime;
                double minutesRemaining = timeUntilSlot.TotalMinutes;

                if (minutesRemaining <= scheduleRule.AutoAcceptMinutes)
                {
                    string terminalCompanyName = slot.Terminal?.CompanyName ?? "Terminal";
                    container.Status = "Accepted";
                    container.AcceptedTime = DateTime.UtcNow;
                    container.UpdateHistory ??= new List<AleContainerAudit>();
                    container.UpdateHistory.Add(new AleContainerAudit
                    {
                        UpdatedBy = terminalCompanyName, 
                        UpdatedTime = DateTime.UtcNow,
                        Action = $"Container accepted by {terminalCompanyName}." 
                    });
                    dbContext.AleContainers.Update(container);
                    
                    string message = $"Container {container.ContainerId.ToString()} has been automatically accepted by {terminalCompanyName} for slot {slot.Time}.";
                    await notifService.CreateNotification(
                        assignment.HaulierId,
                        message,
                        assignment.ROTNumber,
                        assignment.ContainerId);
                }
            }
        }

        await dbContext.SaveChangesAsync();
    }

    private async Task ProcessAutoRejectAsync(ApplicationDbContext dbContext, INotificationService notifService)
    {
        var todayDate = DateOnly.FromDateTime(DateTime.Today);
        
        var expiredAssignments = await dbContext.AleAssignedHauliers
            .Include(a => a.AleContainer)
            .Include(a => a.AleTimeSlot)
            .Include(a => a.AleTimeSlot.Terminal)
            .Where(a => a.AleTimeSlot != null
                && a.AleTimeSlot.Date < todayDate
                && a.AleContainer != null
                && a.AleContainer.GatedInTime == null
                && a.AleContainer.RejectedTime == null
                && a.AleContainer.RejectedBothTime == null
                && (a.AleContainer.Status == "Accepted" ||  a.AleContainer.Status == "Approved-Complete")
                && a.AleContainer.AcceptedTime != null
                && a.AleContainer.ApprovedBothTime != null)
            .ToListAsync();

        foreach (var assignment in expiredAssignments)
        {
            var container = assignment.AleContainer;
            var slot = assignment.AleTimeSlot;
            string terminalName = slot.Terminal?.CompanyName ?? "Terminal";
            
            container.Status = "Rejected";
            container.RejectedTime = DateTime.UtcNow;
            container.RejectedRemarks = "Auto-Rejected: Trucker missed the scheduled booking window date without executing gate-in.";
            container.UpdateHistory ??= new List<AleContainerAudit>();
            container.UpdateHistory.Add(new AleContainerAudit
            {
                UpdatedBy = terminalName, 
                UpdatedTime = DateTime.UtcNow,
                Action = $"Container Auto-Rejected. Reason: Trucker missed the scheduled booking window date without executing gate-in." 
            });
            dbContext.AleContainers.Update(container);
            
            string message = $"Booking for container {container.ContainerId.ToString()} on {slot.Date} ({slot.Time}) was automatically rejected because never gated-in.";
            await notifService.CreateNotification(
                assignment.HaulierId,
                message,
                assignment.ROTNumber,
                assignment.ContainerId);
        }
        await  dbContext.SaveChangesAsync();
    }
}