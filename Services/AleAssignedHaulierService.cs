using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.AleAssignedHaulier;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class AleAssignedHaulierService : IAleAssignedHaulierService
{
    private readonly ApplicationDbContext _dbContext;

    public static AleAssignedHaulierDto MapToDto(AleAssignedHaulier aleAssignedHaulier) => new()
    {
        Id = aleAssignedHaulier.Id,
        PMId = aleAssignedHaulier.PMId,
        PrimeMover =  aleAssignedHaulier.PrimeMover,
        DriverId =  aleAssignedHaulier.DriverId,
        Driver =  aleAssignedHaulier.Driver,
        TimeSlotId = aleAssignedHaulier.TimeSlotId,
        TimeSlot =  aleAssignedHaulier.TimeSlot,
        TrailerId = aleAssignedHaulier.TrailerId,
        Trailer =  aleAssignedHaulier.Trailer,
        ContainerId = aleAssignedHaulier.ContainerId,
        AleContainer = aleAssignedHaulier.AleContainer,
        ROTNumber = aleAssignedHaulier.ROTNumber,
        AleBooking = aleAssignedHaulier.AleBooking,
        HaulierId = aleAssignedHaulier.HaulierId,
        PassNumber =  aleAssignedHaulier.PassNumber,
        ConsigneeTimeSlot =  aleAssignedHaulier.ConsigneeTimeSlot,
    };
    
    public AleAssignedHaulierService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AleAssignedHaulierDto>> GetAllAsync()
    {
        var aleAssignedHauliers = await _dbContext.AleAssignedHauliers
            .Include(a => a.Driver)
            .Include(a => a.PrimeMover)
            .Include(a => a.TimeSlot)
            .Include(a => a.Trailer)
            .ToListAsync();
        return aleAssignedHauliers.Select(MapToDto);
    }

    public async Task<AleAssignedHaulierDto?> GetByIdAsync(int id)
    {
        var AleAssignedHaulier = await _dbContext.AleAssignedHauliers
            .Include(a => a.Driver)
            .Include(a => a.PrimeMover)
            .Include(a => a.TimeSlot)
            .Include(a => a.Trailer)
            .FirstOrDefaultAsync(x => x.Id == id);
        return AleAssignedHaulier == null? null : MapToDto(AleAssignedHaulier);
    }

    public async Task<AleAssignedHaulierDto> CreateAsync(AleAssignedHaulierCreateDto dto)
    {
        var aleAssignedHaulierExists = await _dbContext.AleAssignedHauliers.AnyAsync(a => a.ContainerId == dto.ContainerId && a.ROTNumber == dto.ROTNumber);
        if (aleAssignedHaulierExists) throw new Exception("Cannot assign new haulier to already assigned haulier.");
        
        var AleAssignedHaulier = new Models.AleAssignedHaulier
        {
            PMId = dto.PMId,
            DriverId =  dto.DriverId,
            TimeSlotId =  dto.TimeSlotId,
            TrailerId =  dto.TrailerId,
            ContainerId = dto.ContainerId,
            ROTNumber =  dto.ROTNumber,
            HaulierId =  dto.HaulierId,
            PassNumber =  dto.PassNumber,
            ConsigneeTimeSlot = dto.ConsigneeTimeSlot,
        };
        await _dbContext.AleAssignedHauliers.AddAsync(AleAssignedHaulier);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(AleAssignedHaulier.Id) ??  MapToDto(AleAssignedHaulier);
    }

    public async Task<AleAssignedHaulierDto?> UpdateAsync(int id, AleAssignedHaulierUpdateDto dto)
    {
        var AleAssignedHaulier = await _dbContext.AleAssignedHauliers.FirstOrDefaultAsync(a => a.Id == id);
        if (AleAssignedHaulier == null)
        {
            return null;
        }

        AleAssignedHaulier.PMId = dto.PMId;
        AleAssignedHaulier.DriverId = dto.DriverId;
        AleAssignedHaulier.TimeSlotId = dto.TimeSlotId;
        AleAssignedHaulier.TrailerId = dto.TrailerId;
        AleAssignedHaulier.ContainerId = dto.ContainerId;
        AleAssignedHaulier.ROTNumber = dto.ROTNumber;
        AleAssignedHaulier.HaulierId = dto.HaulierId;
        AleAssignedHaulier.PassNumber = dto.PassNumber;
        AleAssignedHaulier.ConsigneeTimeSlot = dto.ConsigneeTimeSlot;

        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(AleAssignedHaulier.Id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var AleAssignedHaulier = await _dbContext.AleAssignedHauliers
            .FirstOrDefaultAsync(a => a.Id == id);
        if (AleAssignedHaulier == null)
            return false;

        _dbContext.AleAssignedHauliers.Remove(AleAssignedHaulier);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<AleAssignedHaulierDto?> GetAleAssignedHaulierByContainerId(int id)
    {
        var aleAssignedHaulier = await _dbContext.AleAssignedHauliers
            .Include(a => a.Driver)
            .Include(a => a.PrimeMover)
            .Include(a => a.TimeSlot)
            .Include(a => a.Trailer)
            .Where(a => a.ContainerId == id)
            .FirstOrDefaultAsync();
        return aleAssignedHaulier == null? null : MapToDto(aleAssignedHaulier);
    }
}