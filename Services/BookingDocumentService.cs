using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.BookingDocument;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class BookingDocumentService : IBookingDocumentService
{
    private readonly ApplicationDbContext _dbContext;

    public static BookingDocumentDto MapToDto(BookingDocument bookingDocument) => new()
    {
        BookingDocumentId = bookingDocument.BookingDocumentId,
        DocumentType = bookingDocument.DocumentType,
        FileName = bookingDocument.FileName,
        FilePath = bookingDocument.FilePath,
        UploadDate = bookingDocument.UploadDate,
        ROTNumber = bookingDocument.ROTNumber,
        Booking =  bookingDocument.Booking
    };
    
    public BookingDocumentService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BookingDocumentDto>> GetAllAsync()
    {
        var bookingDocuments = await _dbContext.BookingDocuments
            .Include(b => b.Booking)
            .ToListAsync();
        return bookingDocuments.Select(MapToDto);
    }

    public async Task<BookingDocumentDto?> GetByIdAsync(Guid id)
    {
        var bookingDocument = await _dbContext.BookingDocuments
            .Include(b => b.Booking)
            .FirstOrDefaultAsync(x => x.BookingDocumentId == id);
        return bookingDocument == null? null : MapToDto(bookingDocument);
    }

    public async Task<BookingDocumentDto> CreateAsync(BookingDocumentCreateDto dto)
    {
        string safeBookingNo = dto.ROTNumber.Replace("/", "-").Replace("\\", "-");
        var projectRoot = Directory.GetCurrentDirectory();
        var relativePath = Path.Combine("uploads", "bookings", safeBookingNo);
        var absoluteFolderPath = Path.Combine(projectRoot, relativePath);

        if (!Directory.Exists(absoluteFolderPath))
        {
            Directory.CreateDirectory(absoluteFolderPath);
        }

        string finalFilePath = "";
        
        if (dto.File != null)
        {
            var fileNameOnly = Path.GetFileNameWithoutExtension(dto.File.FileName);
            var extension = Path.GetExtension(dto.File.FileName);
            var uniqueFileName = $"{fileNameOnly}_{Guid.NewGuid()}{extension}";
            var absoluteFilePath = Path.Combine(absoluteFolderPath, uniqueFileName);

            using (var stream = new FileStream(absoluteFilePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }
            finalFilePath = $"/uploads/bookings/{safeBookingNo}/{uniqueFileName}";
        }
        
        var bookingDocument = new Models.BookingDocument
        {
            BookingDocumentId = Guid.NewGuid(),
            DocumentType = dto.DocumentType,
            FileName = dto.FileName,
            FilePath = finalFilePath,
            UploadDate = DateTime.UtcNow,
            ROTNumber = dto.ROTNumber,
        };
        await _dbContext.BookingDocuments.AddAsync(bookingDocument);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(bookingDocument.BookingDocumentId) ??  MapToDto(bookingDocument);
    }

    public async Task<BookingDocumentDto?> UpdateAsync(Guid id, string documentType, string? newFileName, IFormFile? file)
    {
        var bookingDocument = await _dbContext.BookingDocuments.FirstOrDefaultAsync(b => b.BookingDocumentId == id);
        if (bookingDocument == null) return null;
        
        bookingDocument.DocumentType = documentType;
        if (file != null)
        {
            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), bookingDocument.FilePath.TrimStart('/'));
            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }
            string safeBookingNo = bookingDocument.ROTNumber.Replace("/", "-").Replace("\\", "-");
            var subFolderPath = Path.Combine("uploads", "bookings", safeBookingNo);
            var absoluteFolderPath = Path.Combine(Directory.GetCurrentDirectory(), subFolderPath);
            if (!Directory.Exists(absoluteFolderPath)) Directory.CreateDirectory(absoluteFolderPath);

            var fileNameOnly = Path.GetFileNameWithoutExtension(bookingDocument.FileName);
            var extension = Path.GetExtension(bookingDocument.FileName);
            var uniqueFileName = $"{fileNameOnly}_{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(absoluteFolderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            bookingDocument.FileName = newFileName ?? file.FileName;
            bookingDocument.FilePath = $"/uploads/bookings/{safeBookingNo}/{uniqueFileName}";
            bookingDocument.UploadDate = DateTime.UtcNow; 
        }
        else if (!string.IsNullOrEmpty(newFileName))
        {
            bookingDocument.FileName = newFileName;
        }

        await _dbContext.SaveChangesAsync();
        return MapToDto(bookingDocument);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var bookingDocument = await _dbContext.BookingDocuments
            .FirstOrDefaultAsync(b => b.BookingDocumentId == id);
        if (bookingDocument == null)
            return false;

        _dbContext.BookingDocuments.Remove(bookingDocument);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<IEnumerable<BookingDocumentDto?>> GetBookingDocumentByBookingNumber(string id)
    {
        var bookingDocuments = await _dbContext.BookingDocuments
            .Include(b => b.Booking)
            .Where(x => x.ROTNumber == id)
            .ToListAsync();
        return bookingDocuments.Select(MapToDto);
    }
}