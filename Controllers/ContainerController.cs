using CLE_BackEnd.DTOs.Container;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/container")]
public class ContainerController : ControllerBase
{
    private readonly IContainerService _containerService;
    
    public ContainerController(IContainerService containerService)
    {
        _containerService = containerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _containerService.GetAllAsync();
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var container = await _containerService.GetByIdAsync(id);
        if (container == null)
            return NotFound(new{ message = $"Container {id} not found" });
        return Ok(container);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] ContainerCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var container = await _containerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id = container.ContainerId}, container);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ContainerUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var container = await _containerService.UpdateAsync(id, dto, dto.UpdatedBy);
            return Ok(container);
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
        var container = await _containerService.DeleteAsync(id);
        if (!container)
        {
            return NotFound(new { message = $"Container {id} not found." });
        }
        return NoContent();
    }
    
    [HttpGet("all/forwarding/{id}")]
    public async Task<IActionResult> GetAllContainersByForwarding(string id)
    {
        var containers = await _containerService.GetAllContainersByForwarding(id);
        return Ok(containers);
    }
    
    [HttpGet("all/haulier/{id}")]
    public async Task<IActionResult> GetAllContainersByHaulier(string id)
    {
        var containers = await _containerService.GetAllContainersByHaulier(id);
        return Ok(containers);
    }
}