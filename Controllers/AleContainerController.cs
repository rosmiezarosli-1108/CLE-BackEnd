using CLE_BackEnd.DTOs.AleContainer;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/aleContainer")]
public class AleContainerController : ControllerBase
{
    private readonly IAleContainerService _aleContainerService;
    
    public AleContainerController(IAleContainerService aleContainerService)
    {
        _aleContainerService = aleContainerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _aleContainerService.GetAllAsync();
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var aleContainer = await _aleContainerService.GetByIdAsync(id);
        if (aleContainer == null)
            return NotFound(new{ message = $"AleContainer {id} not found" });
        return Ok(aleContainer);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] AleContainerCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var aleContainer = await _aleContainerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id = aleContainer.ContainerId}, aleContainer);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AleContainerUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var aleContainer = await _aleContainerService.UpdateAsync(id, dto, dto.UpdatedBy);
            return Ok(aleContainer);
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
        var aleContainer = await _aleContainerService.DeleteAsync(id);
        if (!aleContainer)
        {
            return NotFound(new { message = $"AleContainer {id} not found." });
        }
        return NoContent();
    }
    
    [HttpGet("all/forwarding/{id}")]
    public async Task<IActionResult> GetAllAleContainersByForwarding(string id)
    {
        var aleContainers = await _aleContainerService.GetAllAleContainersByForwarding(id);
        return Ok(aleContainers);
    }
    
    [HttpGet("all/haulier/{id}")]
    public async Task<IActionResult> GetAllAleContainersByHaulier(string id)
    {
        var aleContainers = await _aleContainerService.GetAllAleContainersByHaulier(id);
        return Ok(aleContainers);
    }
    
    [HttpGet("all/bookingAgent/{id}")]
    public async Task<IActionResult> GetAllAleContainersByBookingAgent(string id)
    {
        var aleContainers = await _aleContainerService.GetAllAleContainersByBookingAgent(id);
        return Ok(aleContainers);
    }
    
    [HttpGet("all/consignee/{id}")]
    public async Task<IActionResult> GetAllAleContainersByConsignee(string id)
    {
        var aleContainers = await _aleContainerService.GetAllAleContainersByConsignee(id);
        return Ok(aleContainers);
    }
    
    [HttpGet("action/akps")]
    public async Task<IActionResult> GetContainersForAKPSAction()
    {
        var aleContainers = await _aleContainerService.GetContainersForAKPSAction();
        return Ok(aleContainers);
    }
    
    [HttpGet("action/custom")]
    public async Task<IActionResult> GetContainersForCustomAction()
    {
        var aleContainers = await _aleContainerService.GetContainersForCustomAction();
        return Ok(aleContainers);
    }
    
    [HttpGet("action/terminal")]
    public async Task<IActionResult> GetContainersForTerminalAction()
    {
        var aleContainers = await _aleContainerService.GetContainersForTerminalAction();
        return Ok(aleContainers);
    }
}
