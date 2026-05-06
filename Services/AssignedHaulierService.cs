using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.AssignedHaulier;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class AssignedHaulierService : IAssignedHaulierService
{
    private readonly ApplicationDbContext _dbContext;

    public static AssignedHaulierDto MapToDto(AssignedHaulier assignedHaulier) => new()
    {
        Id = assignedHaulier.Id,
        PMId = assignedHaulier.PMId,
        PrimeMover =  assignedHaulier.PrimeMover,
        DriverId =  assignedHaulier.DriverId,
        Driver =  assignedHaulier.Driver,
        TimeSlotId = assignedHaulier.TimeSlotId,
        TimeSlot =  assignedHaulier.TimeSlot,
        TrailerId = assignedHaulier.TrailerId,
        Trailer =  assignedHaulier.Trailer,
        ContainerId = assignedHaulier.ContainerId,
        ROTNumber =  assignedHaulier.ROTNumber,
        HaulierId =  assignedHaulier.HaulierId,
        //HaulierName = assignedHaulier.
    };
    
    public AssignedHaulierService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AssignedHaulierDto>> GetAllAsync()
    {
        var assignedHauliers = await _dbContext.AssignedHauliers
            .Include(a => a.Driver)
            .Include(a => a.PrimeMover)
            .Include(a => a.TimeSlot)
            .Include(a => a.Trailer)
            .ToListAsync();
        return assignedHauliers.Select(MapToDto);
    }

    public async Task<AssignedHaulierDto?> GetByIdAsync(int id)
    {
        var AssignedHaulier = await _dbContext.AssignedHauliers
            .Include(a => a.Driver)
            .Include(a => a.PrimeMover)
            .Include(a => a.TimeSlot)
            .Include(a => a.Trailer)
            .FirstOrDefaultAsync(x => x.Id == id);
        return AssignedHaulier == null? null : MapToDto(AssignedHaulier);
    }

    public async Task<AssignedHaulierDto> CreateAsync(AssignedHaulierCreateDto dto)
    {
        var assignedHaulierExists = await _dbContext.Containers.AnyAsync(a => a.ContainerId == dto.ContainerId && a.ROTNumber == dto.ROTNumber);
        if (!assignedHaulierExists) throw new Exception("Cannot assign new haulier to already assigned haulier.");
        
        var AssignedHaulier = new Models.AssignedHaulier
        {
            PMId = dto.PMId,
            DriverId =  dto.DriverId,
            TimeSlotId =  dto.TimeSlotId,
            TrailerId =  dto.TrailerId,
            ContainerId = dto.ContainerId,
            ROTNumber =  dto.ROTNumber,
            HaulierId =  dto.HaulierId,
        };
        await _dbContext.AssignedHauliers.AddAsync(AssignedHaulier);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(AssignedHaulier.Id) ??  MapToDto(AssignedHaulier);
    }

    public async Task<AssignedHaulierDto?> UpdateAsync(int id, AssignedHaulierUpdateDto dto)
    {
        var AssignedHaulier = await _dbContext.AssignedHauliers.FirstOrDefaultAsync(a => a.Id == id);
        if (AssignedHaulier == null)
        {
            return null;
        }

        AssignedHaulier.PMId = dto.PMId;
        AssignedHaulier.DriverId = dto.DriverId;
        AssignedHaulier.TimeSlotId = dto.TimeSlotId;
        AssignedHaulier.TrailerId = dto.TrailerId;
        AssignedHaulier.ContainerId = dto.ContainerId;
        AssignedHaulier.ROTNumber = dto.ROTNumber;
        AssignedHaulier.HaulierId = dto.HaulierId;

        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(AssignedHaulier.Id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var AssignedHaulier = await _dbContext.AssignedHauliers
            .FirstOrDefaultAsync(a => a.Id == id);
        if (AssignedHaulier == null)
            return false;

        _dbContext.AssignedHauliers.Remove(AssignedHaulier);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<AssignedHaulierDto?> GetAssignedHaulierByContainerId(int id)
    {
        var assignedHaulier = await _dbContext.AssignedHauliers
            .Include(a => a.Driver)
            .Include(a => a.PrimeMover)
            .Include(a => a.TimeSlot)
            .Include(a => a.Trailer)
            .Where(a => a.ContainerId == id)
            .FirstOrDefaultAsync();
        return assignedHaulier == null? null : MapToDto(assignedHaulier);
    }
}