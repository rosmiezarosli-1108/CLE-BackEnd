using CLE_BackEnd.DTOs.AssignedHaulier;

namespace CLE_BackEnd.Services;

public interface IAssignedHaulierService
{
    Task<IEnumerable<AssignedHaulierDto>> GetAllAsync();
    Task<AssignedHaulierDto?> GetByIdAsync(int id);
    Task<AssignedHaulierDto> CreateAsync(AssignedHaulierCreateDto dto);
    Task<AssignedHaulierDto?> UpdateAsync(int id, AssignedHaulierUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<AssignedHaulierDto?> GetAssignedHaulierByContainerId(int id);
}