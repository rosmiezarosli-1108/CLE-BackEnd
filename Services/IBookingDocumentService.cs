using CLE_BackEnd.DTOs.Booking;
using CLE_BackEnd.DTOs.BookingDocument;

namespace CLE_BackEnd.Services;

public interface IBookingDocumentService
{
    Task<IEnumerable<BookingDocumentDto>> GetAllAsync();
    Task<BookingDocumentDto?> GetByIdAsync(Guid id);
    Task<BookingDocumentDto> CreateAsync(BookingDocumentCreateDto dto);
    Task<BookingDocumentDto> UpdateAsync(Guid id, string documentType, string? newFileName, IFormFile? file);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<BookingDocumentDto?>> GetBookingDocumentByBookingNumber(string id);
}