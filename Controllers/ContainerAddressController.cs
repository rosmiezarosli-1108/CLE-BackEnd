using CLE_BackEnd.DTOs.ContainerAddress;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/containerAddress")]
public class ContainerAddressController : ControllerBase
{
    private readonly IContainerAddressService _containerAddressService;
    
    public ContainerAddressController(IContainerAddressService containerAddressService)
    {
        _containerAddressService = containerAddressService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var containerAddresses = await _containerAddressService.GetAllAsync();
        return Ok(containerAddresses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var containerAddress = await _containerAddressService.GetByIdAsync(id);
        if (containerAddress == null)
            return NotFound(new{ message = $"ContainerAddress {id} not found" });
        return Ok(containerAddress);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] ContainerAddressCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var containerAddress = await _containerAddressService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id = containerAddress.Id}, containerAddress);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ContainerAddressUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var containerAddress = await _containerAddressService.UpdateAsync(id, dto);
            return Ok(containerAddress);
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
        var containerAddress = await _containerAddressService.DeleteAsync(id);
        if (!containerAddress)
        {
            return NotFound(new { message = $"ContainerAddress {id} not found." });
        }
        return NoContent();
    }
}