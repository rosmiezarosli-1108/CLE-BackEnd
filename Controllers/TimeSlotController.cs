using CLE_BackEnd.DTOs.TimeSlot;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/timeSlot")]
public class TimeSlotController : ControllerBase
{
    private readonly ITimeSlotService _timeSlotService;
    
    public TimeSlotController(ITimeSlotService timeSlotService)
    {
        _timeSlotService = timeSlotService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _timeSlotService.GetAllAsync();
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var timeSlot = await _timeSlotService.GetByIdAsync(id);
        if (timeSlot == null)
            return NotFound(new{ message = $"TimeSlot {id} not found" });
        return Ok(timeSlot);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] TimeSlotCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var timeSlot = await _timeSlotService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id = timeSlot.Id}, timeSlot);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TimeSlotUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var timeSlot = await _timeSlotService.UpdateAsync(id, dto);
            return Ok(timeSlot);
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
        var timeSlot = await _timeSlotService.DeleteAsync(id);
        if (!timeSlot)
        {
            return NotFound(new { message = $"TimeSlot {id} not found." });
        }
        return NoContent();
    }
}