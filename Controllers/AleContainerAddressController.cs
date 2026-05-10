using CLE_BackEnd.DTOs.AleContainerAddress;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/aleContainerAddress")]
public class AleContainerAddressController : ControllerBase
{
    private readonly IAleContainerAddressService _aleContainerAddressService;
    
    public AleContainerAddressController(IAleContainerAddressService aleContainerAddressService)
    {
        _aleContainerAddressService = aleContainerAddressService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var aleContainerAddresses = await _aleContainerAddressService.GetAllAsync();
        return Ok(aleContainerAddresses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var aleContainerAddress = await _aleContainerAddressService.GetByIdAsync(id);
        if (aleContainerAddress == null)
            return NotFound(new{ message = $"AleContainerAddress {id} not found" });
        return Ok(aleContainerAddress);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] AleContainerAddressCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var aleContainerAddress = await _aleContainerAddressService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id = aleContainerAddress.Id}, aleContainerAddress);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AleContainerAddressUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var aleContainerAddress = await _aleContainerAddressService.UpdateAsync(id, dto);
            return Ok(aleContainerAddress);
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
        var aleContainerAddress = await _aleContainerAddressService.DeleteAsync(id);
        if (!aleContainerAddress)
        {
            return NotFound(new { message = $"AleContainerAddress {id} not found." });
        }
        return NoContent();
    }
}