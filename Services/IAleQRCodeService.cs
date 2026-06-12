using CLE_BackEnd.DTOs.AleQRCodeDto;

namespace CLE_BackEnd.Services;

public interface IAleQRCodeService
{
    Task<IEnumerable<AleQRCodeDto>> GetAllAsync();
    Task<AleQRCodeDto?> GetByIdAsync(Guid id);
    Task<AleQRCodeDto> GenerateQRCodeAsync(AleQRCodeCreateDto dto);
    Task<AleQRCodeVerificationResultDto> VerifyQRCodeAsync(string QrCode, string scannedById);
    Task<bool> DeleteAsync(Guid id);
}