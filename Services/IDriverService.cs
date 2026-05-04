using System.Runtime.InteropServices;
using CLE_BackEnd.DTOs.Driver;

namespace CLE_BackEnd.Services;

public interface IDriverService
{
    Task<IEnumerable<DriverDto>> GetAllAsync();
    Task<DriverDto?> GetByIdAsync(Guid id);
    Task<DriverDto> CreateAsync(DriverCreateDto dto);
    Task<DriverDto?> UpdateAsync(Guid id, DriverUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}