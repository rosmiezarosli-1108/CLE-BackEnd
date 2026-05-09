using CLE_BackEnd.DTOs.Container;

namespace CLE_BackEnd.Services;

public interface IContainerService
{
    Task<IEnumerable<ContainerDto>> GetAllAsync();
    Task<ContainerDto?> GetByIdAsync(int id);
    Task<ContainerDto> CreateAsync(ContainerCreateDto dto);
    Task<ContainerDto?> UpdateAsync(int id, ContainerUpdateDto dto, string updatedBy);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<ContainerDto>> GetAllContainersByForwarding(string forwarderId);
    Task<IEnumerable<ContainerDto>> GetAllContainersByHaulier(string haulierId);
}
