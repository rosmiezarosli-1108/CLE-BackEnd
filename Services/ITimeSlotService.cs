using CLE_BackEnd.DTOs.TimeSlot;

namespace CLE_BackEnd.Services;

public interface ITimeSlotService
{
    Task<IEnumerable<TimeSlotDto>> GetAllAsync();
    Task<TimeSlotDto?> GetByIdAsync(Guid id);
    Task<TimeSlotDto> CreateAsync(TimeSlotCreateDto dto);
    Task<TimeSlotDto?> UpdateAsync(Guid id, TimeSlotUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}