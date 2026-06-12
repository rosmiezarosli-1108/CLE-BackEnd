using CLE_BackEnd.DTOs.AleAssignedHaulier;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/aleAssignedHaulier")]
public class AleAssignedHaulierController : ControllerBase
{
    private readonly IAleAssignedHaulierService _aleAssignedHaulierService;
    
    public AleAssignedHaulierController(IAleAssignedHaulierService aleAssignedHaulierService)
    {
        _aleAssignedHaulierService = aleAssignedHaulierService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var aleAssignedHauliers = await _aleAssignedHaulierService.GetAllAsync();
        return Ok(aleAssignedHauliers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var aleAssignedHaulier = await _aleAssignedHaulierService.GetByIdAsync(id);
        if (aleAssignedHaulier == null)
            return NotFound(new{ message = $"AleAssignedHaulier {id} not found" });
        return Ok(aleAssignedHaulier);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] AleAssignedHaulierCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var aleAssignedHaulier = await _aleAssignedHaulierService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id = aleAssignedHaulier.Id}, aleAssignedHaulier);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AleAssignedHaulierUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var aleAssignedHaulier = await _aleAssignedHaulierService.UpdateAsync(id, dto);
            return Ok(aleAssignedHaulier);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var aleAssignedHaulier = await _aleAssignedHaulierService.DeleteAsync(id);
        if (!aleAssignedHaulier)
        {
            return NotFound(new { message = $"AleAssignedHaulier {id} not found." });
        }
        return NoContent();
    }
    
    [HttpGet("container/{id}")]
    public async Task<IActionResult> GetAleAssignedHaulierByContainerId(int id)
    {
        var aleAssignedHaulier = await _aleAssignedHaulierService.GetAleAssignedHaulierByContainerId(id);
        if (aleAssignedHaulier == null)
            return Ok(null);
        return Ok(aleAssignedHaulier);
    }
}