using CLE_BackEnd.DTOs.AleAssignedHaulier;

namespace CLE_BackEnd.Services;

public interface IAleAssignedHaulierService
{
    Task<IEnumerable<AleAssignedHaulierDto>> GetAllAsync();
    Task<AleAssignedHaulierDto?> GetByIdAsync(int id);
    Task<AleAssignedHaulierDto> CreateAsync(AleAssignedHaulierCreateDto dto);
    Task<AleAssignedHaulierDto?> UpdateAsync(int id, AleAssignedHaulierUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<AleAssignedHaulierDto?> GetAleAssignedHaulierByContainerId(int id);
}