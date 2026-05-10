using System.Runtime.InteropServices;
using CLE_BackEnd.DTOs.AleContainerAudit;

namespace CLE_BackEnd.Services;

public interface IAleContainerAuditService
{
    Task<IEnumerable<AleContainerAuditDto>> GetAllAsync();
    Task<AleContainerAuditDto?> GetByIdAsync(int id);
    Task<AleContainerAuditDto> CreateAsync(AleContainerAuditCreateDto dto);
    Task<AleContainerAuditDto?> UpdateAsync(int id, AleContainerAuditUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}