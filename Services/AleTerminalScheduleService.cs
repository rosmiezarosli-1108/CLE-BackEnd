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

    public AleTerminalScheduleService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
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
        await GenerateInitialSlotsAsync(template);
        return MapToDto(template);
    }

    private async Task GenerateInitialSlotsAsync(AleTerminalSchedule template)
    {
        var startDate = DateOnly.FromDateTime(DateTime.Today);
        var endDate = startDate.AddDays(30);
        var currentDate = startDate;

        while (currentDate <= endDate)
        {
            await ClearUnbookedSlotsForDateAsync(template.TerminalId, currentDate);
            bool exists = await _dbContext.AleTimeSlots.AnyAsync(s => s.Date == currentDate && s.TerminalId == template.TerminalId);
            if (!exists)
            {
                var rule = GetDayRule(template, currentDate.DayOfWeek);
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
                        if (hasBreak && (loopTime < breakEnd && slotEndTime > breakStart))
                        {
                            loopTime = slotEndTime;
                            continue;
                        }

                        var slot = new Models.AleTimeSlot
                        {
                            Id = Guid.NewGuid(),
                            Date = currentDate,
                            Time = $"{loopTime:HH:mm} - {slotEndTime:HH:mm}",
                            PickUpTotalSlot = template.MaximumPickUpSlots,
                            DropOffTotalSlot = template.MaximumDropOffSlots,
                            TerminalId = template.TerminalId
                        };
                        await _dbContext.AleTimeSlots.AddAsync(slot);
                        loopTime = slotEndTime;
                    }
                }
            }
            currentDate = currentDate.AddDays(1);
        }
        await _dbContext.SaveChangesAsync();
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