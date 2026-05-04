using CLE_BackEnd.DTOs.Booking;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/booking")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bookings = await _bookingService.GetAllAsync();
        return Ok(bookings);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var booking = await _bookingService.GetByIdAsync(id);
        if (booking == null)
            return NotFound(new { message = $"Booking {id} not found" });
        return Ok(booking);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] BookingCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var booking = await _bookingService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = booking.BLOrBookingNumber }, booking);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] BookingUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var booking = await _bookingService.UpdateAsync(id, dto);
            return Ok(booking);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var booking = await _bookingService.DeleteAsync(id);
        if (!booking)
        {
            return NotFound(new { message = $"Booking {id} not found." });
        }

        return NoContent();
    }
    
    [HttpGet("all/forwarding/{id}")]
    public async Task<IActionResult> GetAllBookingsByForwarding(string id)
    {
        var bookings = await _bookingService.GetAllBookingsByForwarding(id);
        return Ok(bookings);
    }
}