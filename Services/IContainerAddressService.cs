using CLE_BackEnd.DTOs;
using CLE_BackEnd.DTOs.ContainerAddress;

namespace CLE_BackEnd.Services;

public interface IContainerAddressService
{
    Task<IEnumerable<ContainerAddressDto>> GetAllAsync();
    Task<ContainerAddressDto?> GetByIdAsync(int id);
    Task<ContainerAddressDto> CreateAsync(ContainerAddressCreateDto dto);
    Task<ContainerAddressDto?> UpdateAsync(int id, ContainerAddressUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}