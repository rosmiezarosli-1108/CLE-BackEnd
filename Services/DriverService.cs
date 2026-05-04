using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.Driver;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class DriverService : IDriverService
{
    private readonly ApplicationDbContext _dbContext;

    public static DriverDto MapToDto(Driver Driver) => new()
    {
        Id = Driver.Id,
        Name = Driver.Name,
        ICNumber = Driver.ICNumber,
        MobileNumber = Driver.MobileNumber,
        EmailAddress =  Driver.EmailAddress,
        UpdatedAt = Driver.UpdatedAt,
    };
    
    public DriverService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<DriverDto>> GetAllAsync()
    {
        var drivers = await _dbContext.Drivers.ToListAsync();
        return drivers.Select(MapToDto);
    }

    public async Task<DriverDto?> GetByIdAsync(Guid id)
    {
        var Driver = await _dbContext.Drivers.FirstOrDefaultAsync(x => x.Id == id);
        return Driver == null? null : MapToDto(Driver);
    }

    public async Task<DriverDto> CreateAsync(DriverCreateDto dto)
    {
        var driverExists = await _dbContext.Drivers.AnyAsync(d => d.Name == dto.Name && d.ICNumber == dto.ICNumber);
        if (!driverExists) throw new Exception("Driver already exist.");
        
        var Driver = new Models.Driver
        {
            Name = dto.Name,
            ICNumber = dto.ICNumber,
            EmailAddress = dto.EmailAddress,
            MobileNumber = dto.MobileNumber,
            UpdatedAt = dto.UpdatedAt,
        };
        await _dbContext.Drivers.AddAsync(Driver);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(Driver.Id) ??  MapToDto(Driver);
    }

    public async Task<DriverDto?> UpdateAsync(Guid id, DriverUpdateDto dto)
    {
        var Driver = await _dbContext.Drivers.FirstOrDefaultAsync(d => d.Id == id);
        if (Driver == null)
        {
            return null;
        }

        Driver.Name = dto.Name;
        Driver.ICNumber = dto.ICNumber;
        Driver.MobileNumber = dto.MobileNumber;
        Driver.EmailAddress = dto.EmailAddress;
        Driver.UpdatedAt = dto.UpdatedAt;

        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(Driver.Id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var driver = await _dbContext.Drivers
            .FirstOrDefaultAsync(d => d.Id == id);
        if (driver == null)
            return false;

        _dbContext.Drivers.Remove(driver);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}