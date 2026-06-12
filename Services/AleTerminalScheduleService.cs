using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.AleTerminalSchedule;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class DayRuleHelper
{
    public string? Start { get; set; }
    public string? End { get; set; }
    public string? BreakStart { get; set; }
    public string? BreakEnd { get; set; }
}

public class AleTerminalScheduleService : IAleTerminalScheduleService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly INotificationService _notifService;

    public AleTerminalScheduleService(ApplicationDbContext dbContext, INotificationService notifService)
    {
        _dbContext = dbContext;
        _notifService = notifService;
    }

    public static AleTerminalScheduleDto MapToDto(AleTerminalSchedule model) => new()
    {
        TerminalId = model.TerminalId,
        MaximumPickUpSlots = model.MaximumPickUpSlots,
        MaximumDropOffSlots = model.MaximumDropOffSlots,
        AutoAcceptMinutes = model.AutoAcceptMinutes,
        AutoRejectMinutes = model.AutoRejectMinutes,
        MonStart = model.MonStart, MonEnd = model.MonEnd, MonBreakStart = model.MonBreakStart, MonBreakEnd = model.MonBreakEnd,
        TueStart = model.TueStart, TueEnd = model.TueEnd, TueBreakStart = model.TueBreakStart, TueBreakEnd = model.TueBreakEnd,
        WedStart = model.WedStart, WedEnd = model.WedEnd, WedBreakStart = model.WedBreakStart, WedBreakEnd = model.WedBreakEnd,
        ThuStart = model.ThuStart, ThuEnd = model.ThuEnd, ThuBreakStart = model.ThuBreakStart, ThuBreakEnd = model.ThuBreakEnd,
        FriStart = model.FriStart, FriEnd = model.FriEnd, FriBreakStart = model.FriBreakStart, FriBreakEnd = model.FriBreakEnd,
        SatStart = model.SatStart, SatEnd = model.SatEnd, SatBreakStart = model.SatBreakStart, SatBreakEnd = model.SatBreakEnd,
        SunStart = model.SunStart, SunEnd = model.SunEnd, SunBreakStart = model.SunBreakStart, SunBreakEnd = model.SunBreakEnd,
    };

    public async Task<AleTerminalScheduleDto?> GetByTerminalIdAsync(string terminalId)
    {
        var template = await _dbContext.AleTerminalSchedules.FirstOrDefaultAsync(x => x.TerminalId == terminalId);
        return template == null ? null : MapToDto(template);
    }

    public async Task<AleTerminalScheduleDto> SaveTemplateAsync(AleTerminalScheduleCreateDto dto)
    {
        var template = await _dbContext.AleTerminalSchedules.FirstOrDefaultAsync(x => x.TerminalId == dto.TerminalId);

        if (template == null)
        {
            template = new AleTerminalSchedule { TerminalId = dto.TerminalId };
            await _dbContext.AleTerminalSchedules.AddAsync(template);
        }
        
        template.MaximumPickUpSlots = dto.MaximumPickUpSlots;
        template.MaximumDropOffSlots = dto.MaximumDropOffSlots;
        template.AutoAcceptMinutes = dto.AutoAcceptMinutes;
        template.AutoRejectMinutes = dto.AutoRejectMinutes;
        
        template.MonStart = dto.MonStart; template.MonEnd = dto.MonEnd; template.MonBreakStart = dto.MonBreakStart; template.MonBreakEnd = dto.MonBreakEnd;
        template.TueStart = dto.TueStart; template.TueEnd = dto.TueEnd; template.TueBreakStart = dto.TueBreakStart; template.TueBreakEnd = dto.TueBreakEnd;
        template.WedStart = dto.WedStart; template.WedEnd = dto.WedEnd; template.WedBreakStart = dto.WedBreakStart; template.WedBreakEnd = dto.WedBreakEnd;
        template.ThuStart = dto.ThuStart; template.ThuEnd = dto.ThuEnd; template.ThuBreakStart = dto.ThuBreakStart; template.ThuBreakEnd = dto.ThuBreakEnd;
        template.FriStart = dto.FriStart; template.FriEnd = dto.FriEnd; template.FriBreakStart = dto.FriBreakStart; template.FriBreakEnd = dto.FriBreakEnd;
        template.SatStart = dto.SatStart; template.SatEnd = dto.SatEnd; template.SatBreakStart = dto.SatBreakStart; template.SatBreakEnd = dto.SatBreakEnd;
        template.SunStart = dto.SunStart; template.SunEnd = dto.SunEnd; template.SunBreakStart = dto.SunBreakStart; template.SunBreakEnd = dto.SunBreakEnd;

        await _dbContext.SaveChangesAsync();
        await GenerateInitialSlotsAsync(template, dto.ChangeRemarks);
        return MapToDto(template);
    }

    private async Task GenerateInitialSlotsAsync(AleTerminalSchedule template, string? changeRemark)
    {
        var startDate = DateOnly.FromDateTime(DateTime.Today);
        var endDate = startDate.AddDays(30);
        var currentDate = startDate;

        while (currentDate <= endDate)
        {
            var existingSlots = await _dbContext.AleTimeSlots
                .Include(t => t.AssignedHauliers)
                .ThenInclude(ah => ah.AleContainer)
                .Where(t => t.TerminalId == template.TerminalId && t.Date == currentDate)
                .ToListAsync();

            var bookedSlots = existingSlots.Where(t => t.AssignedHauliers != null && t.AssignedHauliers.Any()).ToList();
            var unbookedSlots = existingSlots.Where(t => t.AssignedHauliers == null || !t.AssignedHauliers.Any())
                .ToList();

            // Clear old unbooked slots completely
            if (unbookedSlots.Any())
            {
                _dbContext.AleTimeSlots.RemoveRange(unbookedSlots);
                await _dbContext.SaveChangesAsync();
            }

            // Build a list of valid time strings based on the current Terminal rule
            var rule = GetDayRule(template, currentDate.DayOfWeek);
            List<string> validTimeSlotsForNewRules = new List<string>();
            if (!string.IsNullOrEmpty(rule.Start) && !string.IsNullOrEmpty(rule.End))
            {
                var startTime = TimeOnly.Parse(rule.Start);
                var endTime = TimeOnly.Parse(rule.End);
                var hasBreak = !string.IsNullOrEmpty(rule.BreakStart) && !string.IsNullOrEmpty(rule.BreakEnd);
                var breakStart = hasBreak ? TimeOnly.Parse(rule.BreakStart) : TimeOnly.MinValue;
                var breakEnd = hasBreak ? TimeOnly.Parse(rule.BreakEnd) : TimeOnly.MinValue;

                var loopTime = startTime;
                while (loopTime.AddMinutes(30) <= endTime)
                {
                    var slotEndTime = loopTime.AddMinutes(30);
                    string formattedTimeRange = $"{loopTime:HH:mm} - {slotEndTime:HH:mm}";
                    if (hasBreak && (loopTime < breakEnd && slotEndTime > breakStart))
                    {
                        loopTime = slotEndTime;
                        continue;
                    }

                    validTimeSlotsForNewRules.Add(formattedTimeRange);
                    loopTime = slotEndTime;
                }
            }

            // Handle existing booked slots (Cancel if outside hours, Keep if inside)
            foreach (var bookedSlot in bookedSlots)
            {
                bool isStillValid = false;

                if (!string.IsNullOrEmpty(rule.Start) && !string.IsNullOrEmpty(rule.End))
                {
                    var parts = bookedSlot.Time.Split('-');
                    if (parts.Length == 2)
                    {
                        var bookedStart = TimeOnly.Parse(parts[0].Trim());
                        var bookedEnd = TimeOnly.Parse(parts[1].Trim());

                        var ruleStart = TimeOnly.Parse(rule.Start);
                        var ruleEnd = TimeOnly.Parse(rule.End);

                        // Check if it fits inside the new general window limits
                        bool insideOperatingHours = bookedStart >= ruleStart && bookedEnd <= ruleEnd;

                        // Verify it does not overlap with break windows
                        bool isDuringBreak = false;
                        if (!string.IsNullOrEmpty(rule.BreakStart) && !string.IsNullOrEmpty(rule.BreakEnd))
                        {
                            var breakStart = TimeOnly.Parse(rule.BreakStart);
                            var breakEnd = TimeOnly.Parse(rule.BreakEnd);
                            
                            if (bookedStart < breakEnd && bookedEnd > breakStart)
                            {
                                isDuringBreak = true;
                            }
                        }

                        if (insideOperatingHours && !isDuringBreak)
                        {
                            isStillValid = true;
                        }
                    }
                }

                if (!isStillValid)
                {
                    // It's outside the new hours. Cancel it and notify the haulier
                    bookedSlot.IsCancelled = true;
                    bookedSlot.ChangeRemarks = changeRemark;
                    _dbContext.AleTimeSlots.Update(bookedSlot);

                    foreach (var assignment in bookedSlot.AssignedHauliers)
                    {
                        if (assignment.AleContainer != null)
                        {
                            assignment.AleContainer.Status = "Assigned";
                            assignment.AleContainer.EnrouteTime = null;
                            assignment.AleContainer.AssignedTime = DateTime.UtcNow;
                            _dbContext.AleContainers.Update(assignment.AleContainer);
                        }

                        string notificationMessage =
                            $"Terminal changed operating schedule for slot {bookedSlot.Date} ({bookedSlot.Time}). Reason: {changeRemark ?? "Operational Adjustment"}";
                        await _notifService.CreateNotification(
                            assignment.HaulierId,
                            notificationMessage,
                            assignment.ROTNumber,
                            assignment.ContainerId);
                        
                        _dbContext.AleAssignedHauliers.Remove(assignment);
                    }
                }
                else
                {
                    bookedSlot.IsCancelled = false;
                    _dbContext.AleTimeSlots.Update(bookedSlot);
                }
            }

            await _dbContext.SaveChangesAsync();

            // Generate new unbooked time slots for any empty gaps in the hours rule
            if (!string.IsNullOrEmpty(rule.Start) && !string.IsNullOrEmpty(rule.End))
            {
                var startTime = TimeOnly.Parse(rule.Start);
                var endTime = TimeOnly.Parse(rule.End);
                var hasBreak = !string.IsNullOrEmpty(rule.BreakStart) && !string.IsNullOrEmpty(rule.BreakEnd);
                var breakStart = hasBreak ? TimeOnly.Parse(rule.BreakStart) : TimeOnly.MinValue;
                var breakEnd = hasBreak ? TimeOnly.Parse(rule.BreakEnd) : TimeOnly.MinValue;

                var loopTime = startTime;
                while (loopTime.AddMinutes(30) <= endTime)
                {
                    var slotEndTime = loopTime.AddMinutes(30);
                    string formattedTimeRange = $"{loopTime:HH:mm} - {slotEndTime:HH:mm}";

                    if (hasBreak && (loopTime < breakEnd && slotEndTime > breakStart))
                    {
                        loopTime = slotEndTime;
                        continue;
                    }

                    // Don't create a new slot if an active booking exists here
                    bool slotAlreadyExists = bookedSlots.Any(b => b.Time == formattedTimeRange && !b.IsCancelled);
                    if (slotAlreadyExists)
                    {
                        loopTime = slotEndTime;
                        continue;
                    }

                    var newSlot = new Models.AleTimeSlot
                    {
                        Id = Guid.NewGuid(),
                        Date = currentDate,
                        Time = formattedTimeRange,
                        PickUpTotalSlot = template.MaximumPickUpSlots,
                        DropOffTotalSlot = template.MaximumDropOffSlots,
                        TerminalId = template.TerminalId,
                        ChangeRemarks = null,
                        IsCancelled = false
                    };

                    await _dbContext.AleTimeSlots.AddAsync(newSlot);
                    loopTime = slotEndTime;
                }
            }

            await _dbContext.SaveChangesAsync();
            currentDate = currentDate.AddDays(1);
        }
    }

    private async Task ClearUnbookedSlotsForDateAsync(string terminalId, DateOnly date)
    {
        var unbookedSlots = await _dbContext.AleTimeSlots
            .Where(s => s.TerminalId == terminalId && s.Date == date)
            .ToListAsync();

        if (unbookedSlots.Any())
        {
            _dbContext.AleTimeSlots.RemoveRange(unbookedSlots);
            await _dbContext.SaveChangesAsync();
        }
    }

    public DayRuleHelper GetDayRule(AleTerminalSchedule template, DayOfWeek day)
    {
        return day switch
        {
            DayOfWeek.Monday => new DayRuleHelper { Start = template.MonStart, End = template.MonEnd, BreakStart = template.MonBreakStart, BreakEnd = template.MonBreakEnd },
            DayOfWeek.Tuesday => new DayRuleHelper { Start = template.TueStart, End = template.TueEnd, BreakStart = template.TueBreakStart, BreakEnd = template.TueBreakEnd },
            DayOfWeek.Wednesday => new DayRuleHelper { Start = template.WedStart, End = template.WedEnd, BreakStart = template.WedBreakStart, BreakEnd = template.WedBreakEnd },
            DayOfWeek.Thursday => new DayRuleHelper { Start = template.ThuStart, End = template.ThuEnd, BreakStart = template.ThuBreakStart, BreakEnd = template.ThuBreakEnd },
            DayOfWeek.Friday => new DayRuleHelper { Start = template.FriStart, End = template.FriEnd, BreakStart = template.FriBreakStart, BreakEnd = template.FriBreakEnd },
            DayOfWeek.Saturday => new DayRuleHelper { Start = template.SatStart, End = template.SatEnd, BreakStart = template.SatBreakStart, BreakEnd = template.SatBreakEnd },
            DayOfWeek.Sunday => new DayRuleHelper { Start = template.SunStart, End = template.SunEnd, BreakStart = template.SunBreakStart, BreakEnd = template.SunBreakEnd },
            _ => new DayRuleHelper()
        };
    }
}