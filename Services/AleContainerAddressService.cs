using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.AleContainerAddress;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using AleContainerAddressDto = CLE_BackEnd.DTOs.AleContainerAddress.AleContainerAddressDto;

namespace CLE_BackEnd.Services;

public class AleContainerAddressService : IAleContainerAddressService
{
    private readonly ApplicationDbContext _dbContext;

    public static AleContainerAddressDto MapToDto(AleContainerAddress AleContainerAddress) => new()
    {
        Id = AleContainerAddress.Id,
        Address = AleContainerAddress.Address,
        ContainerId = AleContainerAddress.ContainerId,
    };
    
    public AleContainerAddressService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AleContainerAddressDto>> GetAllAsync()
    {
        var aleContainerAddresses = await _dbContext.AleContainerAddresses.ToListAsync();
        return aleContainerAddresses.Select(MapToDto);
    }

    public async Task<AleContainerAddressDto?> GetByIdAsync(int id)
    {
        var AleContainerAddress = await _dbContext.AleContainerAddresses.FirstOrDefaultAsync(x => x.Id == id);
        return AleContainerAddress == null? null : MapToDto(AleContainerAddress);
    }

    public async Task<AleContainerAddressDto> CreateAsync(AleContainerAddressCreateDto dto)
    {
        var containerExists = await _dbContext.Containers.AnyAsync(c => c.ContainerId == dto.ContainerId);
        if (!containerExists) throw new Exception("Cannot add address to a non-existent container.");
        
        var AleContainerAddress = new Models.AleContainerAddress
        {
            Address = dto.Address,
            ContainerId = dto.ContainerId,
        };
        await _dbContext.AleContainerAddresses.AddAsync(AleContainerAddress);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(AleContainerAddress.Id) ??  MapToDto(AleContainerAddress);
    }

    public async Task<AleContainerAddressDto?> UpdateAsync(int id, AleContainerAddressUpdateDto dto)
    {
        var AleContainerAddress = await _dbContext.AleContainerAddresses.FirstOrDefaultAsync(c => c.Id == id);
        if (AleContainerAddress == null)
        {
            return null;
        }

        AleContainerAddress.Address = dto.Address;
        AleContainerAddress.ContainerId = dto.ContainerId;

        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(AleContainerAddress.Id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var AleContainerAddress = await _dbContext.AleContainerAddresses
            .FirstOrDefaultAsync(c => c.Id == id);
        if (AleContainerAddress == null)
            return false;

        _dbContext.AleContainerAddresses.Remove(AleContainerAddress);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}