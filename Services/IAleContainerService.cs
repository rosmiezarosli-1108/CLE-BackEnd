using CLE_BackEnd.DTOs.AleContainer;

namespace CLE_BackEnd.Services;

public interface IAleContainerService
{
    Task<IEnumerable<AleContainerDto>> GetAllAsync();
    Task<AleContainerDto?> GetByIdAsync(int id);
    Task<AleContainerDto> CreateAsync(AleContainerCreateDto dto);
    Task<AleContainerDto?> UpdateAsync(int id, AleContainerUpdateDto dto, string updatedBy);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<AleContainerDto>> GetAllAleContainersByForwarding(string forwarderId);
    Task<IEnumerable<AleContainerDto>> GetAllAleContainersByHaulier(string haulierId);
}