using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.ContainerAddress;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using ContainerAddressDto = CLE_BackEnd.DTOs.ContainerAddress.ContainerAddressDto;

namespace CLE_BackEnd.Services;

public class ContainerAddressService : IContainerAddressService
{
    private readonly ApplicationDbContext _dbContext;

    public static ContainerAddressDto MapToDto(ContainerAddress ContainerAddress) => new()
    {
        Id = ContainerAddress.Id,
        Address = ContainerAddress.Address,
        ContainerId = ContainerAddress.ContainerId,
    };
    
    public ContainerAddressService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ContainerAddressDto>> GetAllAsync()
    {
        var containerAddresses = await _dbContext.ContainerAddresses.ToListAsync();
        return containerAddresses.Select(MapToDto);
    }

    public async Task<ContainerAddressDto?> GetByIdAsync(int id)
    {
        var ContainerAddress = await _dbContext.ContainerAddresses.FirstOrDefaultAsync(x => x.Id == id);
        return ContainerAddress == null? null : MapToDto(ContainerAddress);
    }

    public async Task<ContainerAddressDto> CreateAsync(ContainerAddressCreateDto dto)
    {
        var containerExists = await _dbContext.Containers.AnyAsync(c => c.ContainerId == dto.ContainerId);
        if (!containerExists) throw new Exception("Cannot add address to a non-existent container.");
        
        var ContainerAddress = new Models.ContainerAddress
        {
            Address = dto.Address,
            ContainerId = dto.ContainerId,
        };
        await _dbContext.ContainerAddresses.AddAsync(ContainerAddress);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(ContainerAddress.Id) ??  MapToDto(ContainerAddress);
    }

    public async Task<ContainerAddressDto?> UpdateAsync(int id, ContainerAddressUpdateDto dto)
    {
        var ContainerAddress = await _dbContext.ContainerAddresses.FirstOrDefaultAsync(c => c.Id == id);
        if (ContainerAddress == null)
        {
            return null;
        }

        ContainerAddress.Address = dto.Address;
        ContainerAddress.ContainerId = dto.ContainerId;

        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(ContainerAddress.Id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var ContainerAddress = await _dbContext.ContainerAddresses
            .FirstOrDefaultAsync(c => c.Id == id);
        if (ContainerAddress == null)
            return false;

        _dbContext.ContainerAddresses.Remove(ContainerAddress);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}