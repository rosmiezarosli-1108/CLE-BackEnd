using CLE_BackEnd.DTOs.Booking;

namespace CLE_BackEnd.Services;

public interface IBookingService
{
    Task<IEnumerable<BookingDto>> GetAllAsync();
    Task<BookingDto?> GetByIdAsync(string id);
    Task<BookingDto> CreateAsync(BookingCreateDto dto);
    Task<BookingDto?> UpdateAsync(string id, BookingUpdateDto dto);
    Task<bool> DeleteAsync(string id);
    Task<IEnumerable<BookingDto>> GetAllBookingsByForwarding(string forwarderId);
    Task<IEnumerable<BookingDto>> GetAllBookingsByHaulier(string haulierId);
}