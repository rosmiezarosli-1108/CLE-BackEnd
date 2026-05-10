using CLE_BackEnd.DTOs.AleContainerAudit;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/aleContainerAudit")]
public class AleContainerAuditController : ControllerBase
{
    private readonly IAleContainerAuditService _aleContainerAuditService;
    
    public AleContainerAuditController(IAleContainerAuditService aleContainerAuditService)
    {
        _aleContainerAuditService = aleContainerAuditService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _aleContainerAuditService.GetAllAsync();
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var aleContainerAudit = await _aleContainerAuditService.GetByIdAsync(id);
        if (aleContainerAudit == null)
            return NotFound(new{ message = $"AleContainerAudit {id} not found" });
        return Ok(aleContainerAudit);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] AleContainerAuditCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var aleContainerAudit = await _aleContainerAuditService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id = aleContainerAudit.AuditId}, aleContainerAudit);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AleContainerAuditUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var aleContainerAudit = await _aleContainerAuditService.UpdateAsync(id, dto);
            return Ok(aleContainerAudit);
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
        var aleContainerAudit = await _aleContainerAuditService.DeleteAsync(id);
        if (!aleContainerAudit)
        {
            return NotFound(new { message = $"AleContainerAudit {id} not found." });
        }
        return NoContent();
    }
}
