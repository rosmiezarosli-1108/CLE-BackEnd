using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.TimeSlot;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class TimeSlotService : ITimeSlotService
{
    private readonly ApplicationDbContext _dbContext;

    public static TimeSlotDto MapToDto(TimeSlot TimeSlot) => new()
    {
        Id = TimeSlot.Id,
        Date = TimeSlot.Date,
        Time = TimeSlot.Time,
        PickUpTotalSlot =  TimeSlot.PickUpTotalSlot,
        DropOffTotalSlot = TimeSlot.DropOffTotalSlot,
        Depot =  TimeSlot.Depot,
        DepotId =  TimeSlot.DepotId,
    };
    
    public TimeSlotService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TimeSlotDto>> GetAllAsync()
    {
        var timeSlots = await _dbContext.TimeSlots.ToListAsync();
        return timeSlots.Select(MapToDto);
    }

    public async Task<TimeSlotDto?> GetByIdAsync(Guid id)
    {
        var timeSlot = await _dbContext.TimeSlots.FirstOrDefaultAsync(x => x.Id == id);
        return timeSlot == null? null : MapToDto(timeSlot);
    }

    public async Task<TimeSlotDto> CreateAsync(TimeSlotCreateDto dto)
    {
        var timeSlotExists = await _dbContext.TimeSlots.AnyAsync(t => t.Date == dto.Date && t.Time == dto.Time);
        if (timeSlotExists) throw new Exception("Timeslot already exist.");
        
        var TimeSlot = new Models.TimeSlot
        {
            Date = dto.Date,
            Time = dto.Time,
            PickUpTotalSlot = dto.PickUpTotalSlot,
            DropOffTotalSlot = dto.DropOffTotalSlot,
            DepotId = dto.DepotId,
        };
        await _dbContext.TimeSlots.AddAsync(TimeSlot);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(TimeSlot.Id) ??  MapToDto(TimeSlot);
    }

    public async Task<TimeSlotDto?> UpdateAsync(Guid id, TimeSlotUpdateDto dto)
    {
        var TimeSlot = await _dbContext.TimeSlots.FirstOrDefaultAsync(t => t.Id == id);
        if (TimeSlot == null)
        {
            return null;
        }

        TimeSlot.Date = dto.Date;
        TimeSlot.Time = dto.Time;
        TimeSlot.PickUpTotalSlot = dto.PickUpTotalSlot;
        TimeSlot.DropOffTotalSlot = dto.DropOffTotalSlot;
        TimeSlot.DepotId = dto.DepotId;

        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(TimeSlot.Id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var timeSlot = await _dbContext.TimeSlots
            .FirstOrDefaultAsync(t => t.Id == id);
        if (timeSlot == null)
            return false;

        _dbContext.TimeSlots.Remove(timeSlot);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
