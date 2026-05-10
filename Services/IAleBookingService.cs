using CLE_BackEnd.DTOs;
using CLE_BackEnd.DTOs.AleBooking;

namespace CLE_BackEnd.Services;

public interface IAleBookingService
{
    Task<IEnumerable<AleBookingDto>> GetAllAsync();
    Task<AleBookingDto?> GetByIdAsync(string id);
    Task<AleBookingDto> CreateAsync(AleBookingCreateDto dto);
    Task<AleBookingDto?> UpdateAsync(string id, AleBookingUpdateDto dto);
    Task<bool> DeleteAsync(string id);
    Task<IEnumerable<AleBookingDto>> GetAllAleBookingsByForwarding(string forwarderId);
    Task<IEnumerable<AleBookingDto>> GetAllAleBookingsByHaulier(string haulierId);
}