using CLE_BackEnd.DTOs.AleTimeSlot;

namespace CLE_BackEnd.Services;

public interface IAleTimeSlotService
{
    Task<IEnumerable<AleTimeSlotDto>> GetAllAsync();
    Task<AleTimeSlotDto?> GetByIdAsync(Guid id);
    Task<AleTimeSlotDto> CreateAsync(AleTimeSlotCreateDto dto);
    Task<AleTimeSlotDto?> UpdateAsync(Guid id, AleTimeSlotUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}