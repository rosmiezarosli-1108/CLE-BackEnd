using CLE_BackEnd.DTOs.AleTimeSlot;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/aleTimeSlot")]
public class AleTimeSlotController : ControllerBase
{
    private readonly IAleTimeSlotService _aleTimeSlotService;
    
    public AleTimeSlotController(IAleTimeSlotService aleTimeSlotService)
    {
        _aleTimeSlotService = aleTimeSlotService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _aleTimeSlotService.GetAllAsync();
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var aleTimeSlot = await _aleTimeSlotService.GetByIdAsync(id);
        if (aleTimeSlot == null)
            return NotFound(new{ message = $"AleTimeSlot {id} not found" });
        return Ok(aleTimeSlot);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] AleTimeSlotCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var aleTimeSlot = await _aleTimeSlotService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id = aleTimeSlot.Id}, aleTimeSlot);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] AleTimeSlotUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var aleTimeSlot = await _aleTimeSlotService.UpdateAsync(id, dto);
            return Ok(aleTimeSlot);
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
        var aleTimeSlot = await _aleTimeSlotService.DeleteAsync(id);
        if (!aleTimeSlot)
        {
            return NotFound(new { message = $"AleTimeSlot {id} not found." });
        }
        return NoContent();
    }
}