using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.ContainerAudit;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class ContainerAuditService : IContainerAuditService
{
    private readonly ApplicationDbContext _dbContext;

    public static ContainerAuditDto MapToDto(ContainerAudit ContainerAudit) => new()
    {
        AuditId = ContainerAudit.AuditId,
        ContainerId = ContainerAudit.ContainerId,
        UpdatedBy = ContainerAudit.UpdatedBy,
        UpdatedTime = ContainerAudit.UpdatedTime,
        Action =  ContainerAudit.Action,
        Changes = ContainerAudit.Changes,
    };
    
    public ContainerAuditService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ContainerAuditDto>> GetAllAsync()
    {
        var containerAudits = await _dbContext.ContainerAudits.ToListAsync();
        return containerAudits.Select(MapToDto);
    }

    public async Task<ContainerAuditDto?> GetByIdAsync(int id)
    {
        var ContainerAudit = await _dbContext.ContainerAudits.FirstOrDefaultAsync(x => x.AuditId == id);
        return ContainerAudit == null? null : MapToDto(ContainerAudit);
    }

    public async Task<ContainerAuditDto> CreateAsync(ContainerAuditCreateDto dto)
    {
        var containerAuditExists = await _dbContext.ContainerAudits.AnyAsync(c => c.UpdatedBy == dto.UpdatedBy && c.UpdatedTime == dto.UpdatedTime && c.ContainerId == dto.ContainerId);
        if (containerAuditExists) throw new Exception("ContainerAudit already exist.");
        
        var ContainerAudit = new Models.ContainerAudit
        {
            ContainerId = dto.ContainerId,
            UpdatedBy = dto.UpdatedBy,
            UpdatedTime = dto.UpdatedTime,
            Action = dto.Action,
            Changes = dto.Changes,
        };
        await _dbContext.ContainerAudits.AddAsync(ContainerAudit);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(ContainerAudit.AuditId) ??  MapToDto(ContainerAudit);
    }

    public async Task<ContainerAuditDto?> UpdateAsync(int id, ContainerAuditUpdateDto dto)
    {
        var ContainerAudit = await _dbContext.ContainerAudits.FirstOrDefaultAsync(c => c.AuditId == id);
        if (ContainerAudit == null)
        {
            return null;
        }

        ContainerAudit.ContainerId = dto.ContainerId;
        ContainerAudit.UpdatedBy = dto.UpdatedBy;
        ContainerAudit.UpdatedTime = dto.UpdatedTime;
        ContainerAudit.Action = dto.Action;
        ContainerAudit.Changes = dto.Changes;

        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(ContainerAudit.AuditId);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var containerAudit = await _dbContext.ContainerAudits
            .FirstOrDefaultAsync(c => c.AuditId == id);
        if (containerAudit == null)
            return false;

        _dbContext.ContainerAudits.Remove(containerAudit);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}