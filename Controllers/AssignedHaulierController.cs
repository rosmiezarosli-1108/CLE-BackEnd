using CLE_BackEnd.DTOs.AssignedHaulier;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/assignedHaulier")]
public class AssignedHaulierController : ControllerBase
{
    private readonly IAssignedHaulierService _assignedHaulierService;
    
    public AssignedHaulierController(IAssignedHaulierService assignedHaulierService)
    {
        _assignedHaulierService = assignedHaulierService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var assignedHauliers = await _assignedHaulierService.GetAllAsync();
        return Ok(assignedHauliers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var assignedHaulier = await _assignedHaulierService.GetByIdAsync(id);
        if (assignedHaulier == null)
            return NotFound(new{ message = $"AssignedHaulier {id} not found" });
        return Ok(assignedHaulier);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] AssignedHaulierCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var assignedHaulier = await _assignedHaulierService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id = assignedHaulier.Id}, assignedHaulier);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AssignedHaulierUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var assignedHaulier = await _assignedHaulierService.UpdateAsync(id, dto);
            return Ok(assignedHaulier);
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
        var assignedHaulier = await _assignedHaulierService.DeleteAsync(id);
        if (!assignedHaulier)
        {
            return NotFound(new { message = $"AssignedHaulier {id} not found." });
        }
        return NoContent();
    }
    
    [HttpGet("container/{id}")]
    public async Task<IActionResult> GetAssignedHaulierByContainerId(int id)
    {
        var assignedHaulier = await _assignedHaulierService.GetAssignedHaulierByContainerId(id);
        if (assignedHaulier == null)
            return NotFound(new{ message = $"Assigned Haulier with container ID of {id} not found" });
        return Ok(assignedHaulier);
    }
}