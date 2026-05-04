using CLE_BackEnd.DTOs.BookingDocument;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/bookingDocument")]
public class BookingDocumentController : ControllerBase
{
    private readonly IBookingDocumentService _bookingDocumentService;

    public BookingDocumentController(IBookingDocumentService bookingDocumentService)
    {
        _bookingDocumentService = bookingDocumentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bookingDocuments = await _bookingDocumentService.GetAllAsync();
        return Ok(bookingDocuments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var bookingDocument = await _bookingDocumentService.GetByIdAsync(id);
        if (bookingDocument == null)
            return NotFound(new { message = $"BookingDocument {id} not found" });
        return Ok(bookingDocument);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromForm] BookingDocumentCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var bookingDocument = await _bookingDocumentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = bookingDocument.BookingDocumentId }, bookingDocument);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }
    
    [Consumes("multipart/form-data")]
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(Guid id,[FromForm] BookingDocumentUpdateDto dto)
    {
        try
        {
            var updatedDocument = await _bookingDocumentService.UpdateAsync(id, dto.DocumentType, dto.FileName, dto.File);
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
        var bookingDocument = await _bookingDocumentService.DeleteAsync(id);
        if (!bookingDocument)
        {
            return NotFound(new { message = $"BookingDocument {id} not found." });
        }

        return NoContent();
    }
    
    [HttpGet("booking/{id}")]
    public async Task<IActionResult> GetBookingDocumentByBookingNumber(string id)
    {
        var bookingDocuments = await _bookingDocumentService.GetBookingDocumentByBookingNumber(id);
        return Ok(bookingDocuments);
    }
}