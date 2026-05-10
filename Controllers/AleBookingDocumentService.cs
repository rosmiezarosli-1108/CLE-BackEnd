using CLE_BackEnd.DTOs.AleBookingDocument;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/aleBookingDocument")]
public class AleBookingDocumentController : ControllerBase
{
    private readonly IAleBookingDocumentService _aleBookingDocumentService;

    public AleBookingDocumentController(IAleBookingDocumentService aleBookingDocumentService)
    {
        _aleBookingDocumentService = aleBookingDocumentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var aleBookingDocuments = await _aleBookingDocumentService.GetAllAsync();
        return Ok(aleBookingDocuments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var aleBookingDocument = await _aleBookingDocumentService.GetByIdAsync(id);
        if (aleBookingDocument == null)
            return NotFound(new { message = $"AleBookingDocument {id} not found" });
        return Ok(aleBookingDocument);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromForm] AleBookingDocumentCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var aleBookingDocument = await _aleBookingDocumentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = aleBookingDocument.BookingDocumentId }, aleBookingDocument);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }
    
    [Consumes("multipart/form-data")]
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(Guid id,[FromForm] AleBookingDocumentUpdateDto dto)
    {
        try
        {
            var updatedDocument = await _aleBookingDocumentService.UpdateAsync(id, dto.DocumentType, dto.FileName, dto.File);
            if (updatedDocument == null)
                return NotFound(new { message = "Document not found" });
            return Ok(updatedDocument);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var aleBookingDocument = await _aleBookingDocumentService.DeleteAsync(id);
        if (!aleBookingDocument)
        {
            return NotFound(new { message = $"AleBookingDocument {id} not found." });
        }

        return NoContent();
    }
    
    [HttpGet("booking/{id}")]
    public async Task<IActionResult> GetAleBookingDocumentByBookingNumber(string id)
    {
        var aleBookingDocuments = await _aleBookingDocumentService.GetAleBookingDocumentByBookingNumber(id);
        return Ok(aleBookingDocuments);
    }
}