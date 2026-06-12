using CLE_BackEnd.DTOs.AleQRCodeDto;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/aleQRCode")]
public class AleQRCodeController : ControllerBase
{
    private readonly IAleQRCodeService _aleQRCodeService;
    
    public AleQRCodeController(IAleQRCodeService aleQRCodeService)
    {
        _aleQRCodeService = aleQRCodeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var aleQrCodes = await _aleQRCodeService.GetAllAsync();
        return Ok(aleQrCodes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var aleQRCode = await _aleQRCodeService.GetByIdAsync(id);
        if (aleQRCode == null)
            return NotFound(new{ message = $"AleQRCode {id} not found" });
        return Ok(aleQRCode);
    }

    [HttpPost("generate-qr")]
    public async Task<IActionResult> GenerateQRCode([FromBody] AleQRCodeCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var aleQRCode = await _aleQRCodeService.GenerateQRCodeAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id = aleQRCode.Id}, aleQRCode);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }
    
    [HttpPost("verify-qr")]
    public async Task<IActionResult> VerifyQRCode([FromBody] AleQRCodeVerifyRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var aleQRCode = await _aleQRCodeService.VerifyQRCodeAsync(dto.QRCode, dto.ScannedById);
            if (!aleQRCode.Success)
            {
                return BadRequest(aleQRCode); 
            }
            return Ok(aleQRCode);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var aleQRCode = await _aleQRCodeService.DeleteAsync(id);
        if (!aleQRCode)
        {
            return NotFound(new { message = $"AleQRCode {id} not found." });
        }
        return NoContent();
    }
}