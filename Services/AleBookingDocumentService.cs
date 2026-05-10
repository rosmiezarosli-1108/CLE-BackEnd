using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.AleBookingDocument;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class AleBookingDocumentService : IAleBookingDocumentService
{
    private readonly ApplicationDbContext _dbContext;

    public static AleBookingDocumentDto MapToDto(AleBookingDocument aleBookingDocument) => new()
    {
        BookingDocumentId = aleBookingDocument.BookingDocumentId,
        DocumentType = aleBookingDocument.DocumentType,
        FileName = aleBookingDocument.FileName,
        FilePath = aleBookingDocument.FilePath,
        UploadDate = aleBookingDocument.UploadDate,
        ROTNumber = aleBookingDocument.ROTNumber,
        AleBooking =  aleBookingDocument.AleBooking
    };
    
    public AleBookingDocumentService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AleBookingDocumentDto>> GetAllAsync()
    {
        var aleBookingDocuments = await _dbContext.AleBookingDocuments
            .Include(b => b.AleBooking)
            .ToListAsync();
        return aleBookingDocuments.Select(MapToDto);
    }

    public async Task<AleBookingDocumentDto?> GetByIdAsync(Guid id)
    {
        var aleBookingDocument = await _dbContext.AleBookingDocuments
            .Include(b => b.AleBooking)
            .FirstOrDefaultAsync(x => x.BookingDocumentId == id);
        return aleBookingDocument == null? null : MapToDto(aleBookingDocument);
    }

    public async Task<AleBookingDocumentDto> CreateAsync(AleBookingDocumentCreateDto dto)
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
        
        var aleBookingDocument = new Models.AleBookingDocument
        {
            BookingDocumentId = Guid.NewGuid(),
            DocumentType = dto.DocumentType,
            FileName = dto.FileName,
            FilePath = finalFilePath,
            UploadDate = DateTime.UtcNow,
            ROTNumber = dto.ROTNumber,
        };
        await _dbContext.AleBookingDocuments.AddAsync(aleBookingDocument);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(aleBookingDocument.BookingDocumentId) ??  MapToDto(aleBookingDocument);
    }

    public async Task<AleBookingDocumentDto?> UpdateAsync(Guid id, string documentType, string? newFileName, IFormFile? file)
    {
        var aleBookingDocument = await _dbContext.AleBookingDocuments.FirstOrDefaultAsync(b => b.BookingDocumentId == id);
        if (aleBookingDocument == null) return null;
        
        aleBookingDocument.DocumentType = documentType;
        if (file != null)
        {
            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), aleBookingDocument.FilePath.TrimStart('/'));
            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }
            string safeBookingNo = aleBookingDocument.ROTNumber.Replace("/", "-").Replace("\\", "-");
            var subFolderPath = Path.Combine("uploads", "bookings", safeBookingNo);
            var absoluteFolderPath = Path.Combine(Directory.GetCurrentDirectory(), subFolderPath);
            if (!Directory.Exists(absoluteFolderPath)) Directory.CreateDirectory(absoluteFolderPath);

            var fileNameOnly = Path.GetFileNameWithoutExtension(aleBookingDocument.FileName);
            var extension = Path.GetExtension(aleBookingDocument.FileName);
            var uniqueFileName = $"{fileNameOnly}_{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(absoluteFolderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            aleBookingDocument.FileName = newFileName ?? file.FileName;
            aleBookingDocument.FilePath = $"/uploads/bookings/{safeBookingNo}/{uniqueFileName}";
            aleBookingDocument.UploadDate = DateTime.UtcNow; 
        }
        else if (!string.IsNullOrEmpty(newFileName))
        {
            aleBookingDocument.FileName = newFileName;
        }

        await _dbContext.SaveChangesAsync();
        return MapToDto(aleBookingDocument);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var aleBookingDocument = await _dbContext.AleBookingDocuments
            .FirstOrDefaultAsync(b => b.BookingDocumentId == id);
        if (aleBookingDocument == null)
            return false;

        _dbContext.AleBookingDocuments.Remove(aleBookingDocument);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<IEnumerable<AleBookingDocumentDto?>> GetAleBookingDocumentByBookingNumber(string id)
    {
        var aleBookingDocuments = await _dbContext.AleBookingDocuments
            .Include(b => b.AleBooking)
            .Where(x => x.ROTNumber == id)
            .ToListAsync();
        return aleBookingDocuments.Select(MapToDto);
    }
}