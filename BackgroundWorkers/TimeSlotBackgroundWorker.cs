using CLE_BackEnd.Data;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.BackgroundWorkers;

public class TimeSlotBackgroundWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public TimeSlotBackgroundWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    // Look ahead exactly 30 days from today
                    var targetDate = DateOnly.FromDateTime(DateTime.Today.AddDays(30));
                    var targetDayOfWeek = targetDate.DayOfWeek;

                    // Fetch all terminal layout rule templates 
                    var templates = await dbContext.AleTerminalSchedules.ToListAsync(stoppingToken);

                    foreach (var template in templates)
                    {
                        // 1. Check if slots are already generated for this terminal on that target date
                        bool slotsExist = await dbContext.AleTimeSlots.AnyAsync(
                            s => s.Date == targetDate && s.TerminalId == template.TerminalId, 
                            stoppingToken
                        );

                        if (slotsExist) continue; // Already generated, skip to next terminal

                        // 2. Extract specific day rules using our helper method
                        var rule = GetDayRule(template, targetDayOfWeek);

                        // If user used the trash icon to clear fields, skip generating slots
                        if (string.IsNullOrEmpty(rule.Start) || string.IsNullOrEmpty(rule.End)) continue;

                        var startTime = TimeOnly.Parse(rule.Start);
                        var endTime = TimeOnly.Parse(rule.End);
                        
                        var hasBreak = !string.IsNullOrEmpty(rule.BreakStart) && !string.IsNullOrEmpty(rule.BreakEnd);
                        var breakStart = hasBreak ? TimeOnly.Parse(rule.BreakStart) : TimeOnly.MinValue;
                        var breakEnd = hasBreak ? TimeOnly.Parse(rule.BreakEnd) : TimeOnly.MinValue;

                        var loopTime = startTime;

                        // 3. Increment through windows in 30-minute intervals
                        while (loopTime.AddMinutes(30) <= endTime)
                        {
                            var slotEndTime = loopTime.AddMinutes(30);

                            // Skip break allocations
                            if (hasBreak && (loopTime >= breakStart && slotEndTime <= breakEnd))
                            {
                                loopTime = slotEndTime;
                                continue;
                            }

                            // Injecting directly into your original model structure!
                            var slot = new AleTimeSlot
                            {
                                Id = Guid.NewGuid(),
                                Date = targetDate,
                                Time = $"{loopTime:HH:mm} - {slotEndTime:HH:mm}",
                                PickUpTotalSlot = template.MaximumPickUpSlots,
                                DropOffTotalSlot = template.MaximumDropOffSlots,
                                TerminalId = template.TerminalId
                            };

                            dbContext.AleTimeSlots.Add(slot);
                            loopTime = slotEndTime;
                        }
                    }

                    await dbContext.SaveChangesAsync(stoppingToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running schedule generator background task: {ex.Message}");
            }

            await Task.Delay(TimeSpan.FromHours(12), stoppingToken);
        }
    }

    private DayRuleWorkerHelper GetDayRule(AleTerminalSchedule template, DayOfWeek day)
    {
        return day switch
        {
            DayOfWeek.Monday => new DayRuleWorkerHelper { Start = template.MonStart, End = template.MonEnd, BreakStart = template.MonBreakStart, BreakEnd = template.MonBreakEnd },
            DayOfWeek.Tuesday => new DayRuleWorkerHelper { Start = template.TueStart, End = template.TueEnd, BreakStart = template.TueBreakStart, BreakEnd = template.TueBreakEnd },
            DayOfWeek.Wednesday => new DayRuleWorkerHelper { Start = template.WedStart, End = template.WedEnd, BreakStart = template.WedBreakStart, BreakEnd = template.WedBreakEnd },
            DayOfWeek.Thursday => new DayRuleWorkerHelper { Start = template.ThuStart, End = template.ThuEnd, BreakStart = template.ThuBreakStart, BreakEnd = template.ThuBreakEnd },
            DayOfWeek.Friday => new DayRuleWorkerHelper { Start = template.FriStart, End = template.FriEnd, BreakStart = template.FriBreakStart, BreakEnd = template.FriBreakEnd },
            DayOfWeek.Saturday => new DayRuleWorkerHelper { Start = template.SatStart, End = template.SatEnd, BreakStart = template.SatBreakStart, BreakEnd = template.SatBreakEnd },
            DayOfWeek.Sunday => new DayRuleWorkerHelper { Start = template.SunStart, End = template.SunEnd, BreakStart = template.SunBreakStart, BreakEnd = template.SunBreakEnd },
            _ => new DayRuleWorkerHelper()
        };
    }
}

public class DayRuleWorkerHelper
{
    public string? Start { get; set; }
    public string? End { get; set; }
    public string? BreakStart { get; set; }
    public string? BreakEnd { get; set; }
}