using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.AleTimeSlot;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class AleTimeSlotService : IAleTimeSlotService
{
    private readonly ApplicationDbContext _dbContext;

    public static AleTimeSlotDto MapToDto(AleTimeSlot AleTimeSlot) => new()
    {
        Id = AleTimeSlot.Id,
        Date = AleTimeSlot.Date,
        Time = AleTimeSlot.Time,
        PickUpTotalSlot = AleTimeSlot.PickUpTotalSlot,
        DropOffTotalSlot = AleTimeSlot.DropOffTotalSlot,
        Terminal = AleTimeSlot.Terminal,
        TerminalId = AleTimeSlot.TerminalId,
    };
    
    public AleTimeSlotService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AleTimeSlotDto>> GetAllAsync()
    {
        var aleTimeSlots = await _dbContext.AleTimeSlots.ToListAsync();
        return aleTimeSlots.Select(MapToDto);
    }

    public async Task<AleTimeSlotDto?> GetByIdAsync(Guid id)
    {
        var aleTimeSlot = await _dbContext.AleTimeSlots.FirstOrDefaultAsync(x => x.Id == id);
        return aleTimeSlot == null? null : MapToDto(aleTimeSlot);
    }

    public async Task<AleTimeSlotDto> CreateAsync(AleTimeSlotCreateDto dto)
    {
        var aleTimeSlotExists = await _dbContext.AleTimeSlots.AnyAsync(t => t.Date == dto.Date && t.Time == dto.Time);
        if (aleTimeSlotExists) throw new Exception("Timeslot already exist.");
        
        var AleTimeSlot = new Models.AleTimeSlot
        {
            Date = dto.Date,
            Time = dto.Time,
            PickUpTotalSlot = dto.PickUpTotalSlot,
            DropOffTotalSlot = dto.DropOffTotalSlot,
            TerminalId = dto.TerminalId,
        };
        await _dbContext.AleTimeSlots.AddAsync(AleTimeSlot);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(AleTimeSlot.Id) ??  MapToDto(AleTimeSlot);
    }

    public async Task<AleTimeSlotDto?> UpdateAsync(Guid id, AleTimeSlotUpdateDto dto)
    {
        var AleTimeSlot = await _dbContext.AleTimeSlots.FirstOrDefaultAsync(t => t.Id == id);
        if (AleTimeSlot == null)
        {
            return null;
        }

        AleTimeSlot.Date = dto.Date;
        AleTimeSlot.Time = dto.Time;
        AleTimeSlot.PickUpTotalSlot = dto.PickUpTotalSlot;
        AleTimeSlot.DropOffTotalSlot = dto.DropOffTotalSlot;
        AleTimeSlot.TerminalId = dto.TerminalId;

        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(AleTimeSlot.Id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var aleTimeSlot = await _dbContext.AleTimeSlots
            .FirstOrDefaultAsync(t => t.Id == id);
        if (aleTimeSlot == null)
            return false;

        _dbContext.AleTimeSlots.Remove(aleTimeSlot);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}

