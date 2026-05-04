using CLE_BackEnd.DTOs.Trailer;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/trailer")]
public class TrailerController : ControllerBase
{
    private readonly ITrailerService _trailerService;
    
    public TrailerController(ITrailerService trailerService)
    {
        _trailerService = trailerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _trailerService.GetAllAsync();
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var trailer = await _trailerService.GetByIdAsync(id);
        if (trailer == null)
            return NotFound(new{ message = $"Trailer {id} not found" });
        return Ok(trailer);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] TrailerCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var trailer = await _trailerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id = trailer.Id}, trailer);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TrailerUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var trailer = await _trailerService.UpdateAsync(id, dto);
            return Ok(trailer);
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
        var trailer = await _trailerService.DeleteAsync(id);
        if (!trailer)
        {
            return NotFound(new { message = $"Trailer {id} not found." });
        }
        return NoContent();
    }
}