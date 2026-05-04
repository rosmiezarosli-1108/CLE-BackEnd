using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.PrimeMover;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class PrimeMoverService : IPrimeMoverService
{
    private readonly ApplicationDbContext _dbContext;

    public static PrimeMoverDto MapToDto(PrimeMover PrimeMover) => new()
    {
        Id = PrimeMover.Id,
        PlateNumber = PrimeMover.PlateNumber,
        PMCode = PrimeMover.PMCode,
        BTM = PrimeMover.BTM,
        BGK =  PrimeMover.BGK,
        DefaultDriver =  PrimeMover.DefaultDriver,
        UpdatedAt = PrimeMover.UpdatedAt,
    };
    
    public PrimeMoverService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<PrimeMoverDto>> GetAllAsync()
    {
        var primeMovers = await _dbContext.PrimeMovers.ToListAsync();
        return primeMovers.Select(MapToDto);
    }

    public async Task<PrimeMoverDto?> GetByIdAsync(Guid id)
    {
        var primeMover = await _dbContext.PrimeMovers.FirstOrDefaultAsync(x => x.Id == id);
        return primeMover == null? null : MapToDto(primeMover);
    }

    public async Task<PrimeMoverDto> CreateAsync(PrimeMoverCreateDto dto)
    {
        var primeMoverExists = await _dbContext.PrimeMovers.AnyAsync(p => p.PlateNumber == dto.PlateNumber && p.PMCode == dto.PMCode);
        if (!primeMoverExists) throw new Exception("Prime Mover already exist.");
        
        var PrimeMover = new Models.PrimeMover
        {
            PlateNumber = dto.PlateNumber,
            PMCode = dto.PMCode,
            BGK = dto.BGK,
            BTM = dto.BTM,
            DefaultDriver =  dto.DefaultDriver,
            UpdatedAt = dto.UpdatedAt,
        };
        await _dbContext.PrimeMovers.AddAsync(PrimeMover);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(PrimeMover.Id) ??  MapToDto(PrimeMover);
    }

    public async Task<PrimeMoverDto?> UpdateAsync(Guid id, PrimeMoverUpdateDto dto)
    {
        var PrimeMover = await _dbContext.PrimeMovers.FirstOrDefaultAsync(p => p.Id == id);
        if (PrimeMover == null)
        {
            return null;
        }

        PrimeMover.PlateNumber = dto.PlateNumber;
        PrimeMover.PMCode = dto.PMCode;
        PrimeMover.BTM = dto.BTM;
        PrimeMover.BGK = dto.BGK;
        PrimeMover.DefaultDriver = dto.DefaultDriver;
        PrimeMover.UpdatedAt = dto.UpdatedAt;

        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(PrimeMover.Id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var primeMover = await _dbContext.PrimeMovers
            .FirstOrDefaultAsync(p => p.Id == id);
        if (primeMover == null)
            return false;

        _dbContext.PrimeMovers.Remove(primeMover);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}