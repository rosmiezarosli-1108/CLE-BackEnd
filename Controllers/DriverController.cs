using CLE_BackEnd.DTOs.Driver;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/driver")]
public class DriverController : ControllerBase
{
    private readonly IDriverService _driverService;
    
    public DriverController(IDriverService driverService)
    {
        _driverService = driverService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _driverService.GetAllAsync();
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var driver = await _driverService.GetByIdAsync(id);
        if (driver == null)
            return NotFound(new{ message = $"Driver {id} not found" });
        return Ok(driver);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] DriverCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var driver = await _driverService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id = driver.Id}, driver);
        }
        catch (Exception ex)
        {
            string cleanMessage = ex.Message;
            return BadRequest(new { message = cleanMessage });
        }
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DriverUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var driver = await _driverService.UpdateAsync(id, dto);
            return Ok(driver);
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
        var driver = await _driverService.DeleteAsync(id);
        if (!driver)
        {
            return NotFound(new { message = $"Driver {id} not found." });
        }
        return NoContent();
    }
}