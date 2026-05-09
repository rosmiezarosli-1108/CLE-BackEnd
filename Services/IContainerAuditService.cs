using System.Runtime.InteropServices;
using CLE_BackEnd.DTOs.ContainerAudit;

namespace CLE_BackEnd.Services;

public interface IContainerAuditService
{
    Task<IEnumerable<ContainerAuditDto>> GetAllAsync();
    Task<ContainerAuditDto?> GetByIdAsync(int id);
    Task<ContainerAuditDto> CreateAsync(ContainerAuditCreateDto dto);
    Task<ContainerAuditDto?> UpdateAsync(int id, ContainerAuditUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
