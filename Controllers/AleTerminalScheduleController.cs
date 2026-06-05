using CLE_BackEnd.DTOs.AleTerminalSchedule;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/aleTerminalSchedule")]
public class AleTerminalScheduleController : ControllerBase
{
    private readonly IAleTerminalScheduleService _templateService;

    public AleTerminalScheduleController(IAleTerminalScheduleService templateService)
    {
        _templateService = templateService;
    }

    [HttpGet("{terminalId}")]
    public async Task<IActionResult> GetByTerminalId(string terminalId)
    {
        var template = await _templateService.GetByTerminalIdAsync(terminalId);
        if (template == null)
            return NotFound(new { message = $"No schedule rules template found for terminal {terminalId}." });
            
        return Ok(template);
    }

    [HttpPost("save")]
    public async Task<IActionResult> SaveTemplate([FromBody] AleTerminalScheduleCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _templateService.SaveTemplateAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}