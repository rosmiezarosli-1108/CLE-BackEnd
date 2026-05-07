using CLE_BackEnd.DTOs.ContainerAudit;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/containerAudit")]
public class ContainerAuditController : ControllerBase
{
    private readonly IContainerAuditService _containerAuditService;
    
    public ContainerAuditController(IContainerAuditService containerAuditService)
    {
        _containerAuditService = containerAuditService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _containerAuditService.GetAllAsync();
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var containerAudit = await _containerAuditService.GetByIdAsync(id);
        if (containerAudit == null)
            return NotFound(new{ message = $"ContainerAudit {id} not found" });
        return Ok(containerAudit);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] ContainerAuditCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var containerAudit = await _containerAuditService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id = containerAudit.AuditId}, containerAudit);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ContainerAuditUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var containerAudit = await _containerAuditService.UpdateAsync(id, dto);
            return Ok(containerAudit);
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
        var containerAudit = await _containerAuditService.DeleteAsync(id);
        if (!containerAudit)
        {
            return NotFound(new { message = $"ContainerAudit {id} not found." });
        }
        return NoContent();
    }
}