using CLE_BackEnd.DTOs.PrimeMover;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/primeMover")]
public class PrimeMoverController : ControllerBase
{
    private readonly IPrimeMoverService _primeMoverService;
    
    public PrimeMoverController(IPrimeMoverService primeMoverService)
    {
        _primeMoverService = primeMoverService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _primeMoverService.GetAllAsync();
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var primeMover = await _primeMoverService.GetByIdAsync(id);
        if (primeMover == null)
            return NotFound(new{ message = $"PrimeMover {id} not found" });
        return Ok(primeMover);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] PrimeMoverCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var primeMover = await _primeMoverService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id = primeMover.Id}, primeMover);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PrimeMoverUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var primeMover = await _primeMoverService.UpdateAsync(id, dto);
            return Ok(primeMover);
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
        var primeMover = await _primeMoverService.DeleteAsync(id);
        if (!primeMover)
        {
            return NotFound(new { message = $"PrimeMover {id} not found." });
        }
        return NoContent();
    }
}