using CLE_BackEnd.DTOs.AleBooking;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/aleBooking")]
public class AleBookingController : ControllerBase
{
    private readonly IAleBookingService _aleBookingService;

    public AleBookingController(IAleBookingService aleBookingService)
    {
        _aleBookingService = aleBookingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var aleBookings = await _aleBookingService.GetAllAsync();
        return Ok(aleBookings);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var aleBooking = await _aleBookingService.GetByIdAsync(id);
        if (aleBooking == null)
            return NotFound(new { message = $"AleBooking {id} not found" });
        return Ok(aleBooking);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] AleBookingCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var aleBooking = await _aleBookingService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = aleBooking.ROTNumber }, aleBooking);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] AleBookingUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var aleBooking = await _aleBookingService.UpdateAsync(id, dto);
            return Ok(aleBooking);
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
        var aleBooking = await _aleBookingService.DeleteAsync(id);
        if (!aleBooking)
        {
            return NotFound(new { message = $"AleBooking {id} not found." });
        }

        return NoContent();
    }
    
    [HttpGet("all/forwarding/{id}")]
    public async Task<IActionResult> GetAllAleBookingsByForwarding(string id)
    {
        var aleBookings = await _aleBookingService.GetAllAleBookingsByForwarding(id);
        return Ok(aleBookings);
    }
    
    [HttpGet("all/haulier/{id}")]
    public async Task<IActionResult> GetAllAleBookingsByHaulier(string id)
    {
        var aleBookings = await _aleBookingService.GetAllAleBookingsByHaulier(id);
        return Ok(aleBookings);
    }
    
    [HttpGet("all/bookingAgent/{id}")]
    public async Task<IActionResult> GetAllAleBookingsByBookingAgent(string id)
    {
        var aleBookings = await _aleBookingService.GetAllAleBookingsByBookingAgent(id);
        return Ok(aleBookings);
    }
    
    [HttpGet("all/consignee/{id}")]
    public async Task<IActionResult> GetAllAleBookingsByConsignee(string id)
    {
        var aleBookings = await _aleBookingService.GetAllAleBookingsByConsignee(id);
        return Ok(aleBookings);
    }
}