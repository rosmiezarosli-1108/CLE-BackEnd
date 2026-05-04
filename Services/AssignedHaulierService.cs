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
        PMNumber = assignedHaulier.PMNumber,
        DriverName =  assignedHaulier.DriverName,
        TimeSlot =  assignedHaulier.TimeSlot,
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
        var assignedHauliers = await _dbContext.AssignedHauliers.ToListAsync();
        return assignedHauliers.Select(MapToDto);
    }

    public async Task<AssignedHaulierDto?> GetByIdAsync(int id)
    {
        var AssignedHaulier = await _dbContext.AssignedHauliers.FirstOrDefaultAsync(x => x.Id == id);
        return AssignedHaulier == null? null : MapToDto(AssignedHaulier);
    }

    public async Task<AssignedHaulierDto> CreateAsync(AssignedHaulierCreateDto dto)
    {
        var assignedHaulierExists = await _dbContext.Containers.AnyAsync(a => a.ContainerId == dto.ContainerId && a.ROTNumber == dto.ROTNumber);
        if (!assignedHaulierExists) throw new Exception("Cannot assign new haulier to already assigned haulier.");
        
        var AssignedHaulier = new Models.AssignedHaulier
        {
            PMNumber = dto.PMNumber,
            DriverName =  dto.DriverName,
            TimeSlot =  dto.TimeSlot,
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

        AssignedHaulier.PMNumber = dto.PMNumber;
        AssignedHaulier.DriverName = dto.DriverName;
        AssignedHaulier.TimeSlot = dto.TimeSlot;
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
}