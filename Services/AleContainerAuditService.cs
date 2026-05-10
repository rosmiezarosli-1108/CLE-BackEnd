using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.AleContainerAudit;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class AleContainerAuditService : IAleContainerAuditService
{
    private readonly ApplicationDbContext _dbContext;

    public static AleContainerAuditDto MapToDto(AleContainerAudit AleContainerAudit) => new()
    {
        AuditId = AleContainerAudit.AuditId,
        ContainerId = AleContainerAudit.ContainerId,
        UpdatedBy = AleContainerAudit.UpdatedBy,
        UpdatedTime = AleContainerAudit.UpdatedTime,
        Action =  AleContainerAudit.Action,
        Changes = AleContainerAudit.Changes,
    };
    
    public AleContainerAuditService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AleContainerAuditDto>> GetAllAsync()
    {
        var aleContainerAudits = await _dbContext.AleContainerAudits.ToListAsync();
        return aleContainerAudits.Select(MapToDto);
    }

    public async Task<AleContainerAuditDto?> GetByIdAsync(int id)
    {
        var AleContainerAudit = await _dbContext.AleContainerAudits.FirstOrDefaultAsync(x => x.AuditId == id);
        return AleContainerAudit == null? null : MapToDto(AleContainerAudit);
    }

    public async Task<AleContainerAuditDto> CreateAsync(AleContainerAuditCreateDto dto)
    {
        var aleContainerAuditExists = await _dbContext.AleContainerAudits.AnyAsync(c => c.UpdatedBy == dto.UpdatedBy && c.UpdatedTime == dto.UpdatedTime && c.ContainerId == dto.ContainerId);
        if (aleContainerAuditExists) throw new Exception("AleContainerAudit already exist.");
        
        var AleContainerAudit = new Models.AleContainerAudit
        {
            ContainerId = dto.ContainerId,
            UpdatedBy = dto.UpdatedBy,
            UpdatedTime = dto.UpdatedTime,
            Action = dto.Action,
            Changes = dto.Changes,
        };
        await _dbContext.AleContainerAudits.AddAsync(AleContainerAudit);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(AleContainerAudit.AuditId) ??  MapToDto(AleContainerAudit);
    }

    public async Task<AleContainerAuditDto?> UpdateAsync(int id, AleContainerAuditUpdateDto dto)
    {
        var AleContainerAudit = await _dbContext.AleContainerAudits.FirstOrDefaultAsync(c => c.AuditId == id);
        if (AleContainerAudit == null)
        {
            return null;
        }

        AleContainerAudit.ContainerId = dto.ContainerId;
        AleContainerAudit.UpdatedBy = dto.UpdatedBy;
        AleContainerAudit.UpdatedTime = dto.UpdatedTime;
        AleContainerAudit.Action = dto.Action;
        AleContainerAudit.Changes = dto.Changes;

        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(AleContainerAudit.AuditId);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var aleContainerAudit = await _dbContext.AleContainerAudits
            .FirstOrDefaultAsync(c => c.AuditId == id);
        if (aleContainerAudit == null)
            return false;

        _dbContext.AleContainerAudits.Remove(aleContainerAudit);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}