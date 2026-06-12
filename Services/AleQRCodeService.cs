using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.AleQRCodeDto;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class AleQRCodeService : IAleQRCodeService
{
    private readonly ApplicationDbContext _dbContext;

    public static AleQRCodeDto MapToDto(AleQRCode AleQRCode) => new()
    {
        Id = AleQRCode.Id,
        QRCode = AleQRCode.QRCode,
        Status = AleQRCode.Status,
        CreatedAt = AleQRCode.CreatedAt,
        VerifiedAt = AleQRCode.VerifiedAt,
        Terminal = AleQRCode.Terminal,
        TerminalId = AleQRCode.TerminalId,
        ScannedById = AleQRCode.ScannedById,
        ScannedBy = AleQRCode.ScannedBy,
        ContainerId = AleQRCode.ContainerId,
        Container = AleQRCode.Container,
    };
    
    public AleQRCodeService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AleQRCodeDto>> GetAllAsync()
    {
        var aleQRCodes = await _dbContext.AleQRCodes
            .Include(qr => qr.Terminal)
            .Include(qr => qr.ScannedBy)
            .Include(qr => qr.Container)
            .ToListAsync();
        return aleQRCodes.Select(MapToDto);
    }

    public async Task<AleQRCodeDto?> GetByIdAsync(Guid id)
    {
        var aleQRCode = await _dbContext.AleQRCodes
            .Include(qr => qr.Terminal)
            .Include(qr => qr.ScannedBy)
            .Include(qr => qr.Container)
            .FirstOrDefaultAsync(x => x.Id == id);
        return aleQRCode == null? null : MapToDto(aleQRCode);
    }

    public async Task<AleQRCodeDto> GenerateQRCodeAsync(AleQRCodeCreateDto dto)
    {
        string uniqueQRCode = Guid.NewGuid().ToString();

        var aleQRCode = new Models.AleQRCode
        {
            Id = Guid.NewGuid(),
            QRCode = uniqueQRCode,
            Status = "Active",
            CreatedAt = DateTime.UtcNow,
            TerminalId = dto.TerminalId,
            ContainerId = dto.ContainerId
        };
        
        await _dbContext.AleQRCodes.AddAsync(aleQRCode);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(aleQRCode.Id) ??  MapToDto(aleQRCode);
    }

    public async Task<AleQRCodeVerificationResultDto> VerifyQRCodeAsync(string QrCode, string scannedById)
    {
        var result = await _dbContext.AleQRCodes
            .Include(qr => qr.Terminal)
            .FirstOrDefaultAsync(qr => qr.QRCode == QrCode);

        if (result == null)
        {
            return new AleQRCodeVerificationResultDto
            {
                Success = false,
                Message = "Invalid QR Code identifier!"
            };
        }
        
        if (result.Status == "Verified" || result.VerifiedAt != null)
        {
            return new AleQRCodeVerificationResultDto
            {
                Success = false,
                Status = result.Status,
                VerifiedAt = result.VerifiedAt,
                TerminalName = result.Terminal?.CompanyName ?? "Unknown Terminal",
                Message = "This QR Code has already been scanned previously!"
            };
        } 
        
        result.Status = "Verified";
        result.VerifiedAt = DateTime.UtcNow;
        result.ScannedById = scannedById;
        
        await _dbContext.SaveChangesAsync();

        return new AleQRCodeVerificationResultDto
        {
            Success = true,
            Status = result.Status,
            VerifiedAt = result.VerifiedAt,
            TerminalName = result.Terminal?.CompanyName ?? "Unknown Terminal",
            Message = "QR Code verification processed successfully. "
        };
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        var aleQRCode = await _dbContext.AleQRCodes
            .FirstOrDefaultAsync(t => t.Id == id);
        if (aleQRCode == null)
            return false;

        _dbContext.AleQRCodes.Remove(aleQRCode);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}