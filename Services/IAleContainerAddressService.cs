using CLE_BackEnd.DTOs;
using CLE_BackEnd.DTOs.AleContainerAddress;

namespace CLE_BackEnd.Services;

public interface IAleContainerAddressService
{
    Task<IEnumerable<AleContainerAddressDto>> GetAllAsync();
    Task<AleContainerAddressDto?> GetByIdAsync(int id);
    Task<AleContainerAddressDto> CreateAsync(AleContainerAddressCreateDto dto);
    Task<AleContainerAddressDto?> UpdateAsync(int id, AleContainerAddressUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}