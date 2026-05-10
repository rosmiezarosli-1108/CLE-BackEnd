using CLE_BackEnd.DTOs.AleBookingDocument;
using CLE_BackEnd.DTOs.BookingDocument;

namespace CLE_BackEnd.Services;

public interface IAleBookingDocumentService
{
    Task<IEnumerable<AleBookingDocumentDto>> GetAllAsync();
    Task<AleBookingDocumentDto?> GetByIdAsync(Guid id);
    Task<AleBookingDocumentDto> CreateAsync(AleBookingDocumentCreateDto dto);
    Task<AleBookingDocumentDto> UpdateAsync(Guid id, string documentType, string? newFileName, IFormFile? file);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<AleBookingDocumentDto?>> GetAleBookingDocumentByBookingNumber(string id);
}