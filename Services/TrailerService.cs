using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.Trailer;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class TrailerService : ITrailerService
{
    private readonly ApplicationDbContext _dbContext;

    public static TrailerDto MapToDto(Trailer Trailer) => new()
    {
        Id = Trailer.Id,
        PlateNumber = Trailer.PlateNumber,
        Type = Trailer.Type,
        BTM = Trailer.BTM,
        UpdatedAt = Trailer.UpdatedAt,
        HaulierId = Trailer.HaulierId,
    };
    
    public TrailerService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TrailerDto>> GetAllAsync()
    {
        var trailers = await _dbContext.Trailers.ToListAsync();
        return trailers.Select(MapToDto);
    }

    public async Task<TrailerDto?> GetByIdAsync(Guid id)
    {
        var trailer = await _dbContext.Trailers.FirstOrDefaultAsync(x => x.Id == id);
        return trailer == null? null : MapToDto(trailer);
    }

    public async Task<TrailerDto> CreateAsync(TrailerCreateDto dto)
    {
        var trailerExists = await _dbContext.Trailers.AnyAsync(t => t.PlateNumber == dto.PlateNumber && t.Type == dto.Type);
        if (trailerExists) throw new Exception("Trailer already exist.");
        
        var Trailer = new Models.Trailer
        {
            PlateNumber = dto.PlateNumber,
            Type = dto.Type,
            BTM = dto.BTM,
            UpdatedAt = dto.UpdatedAt,
            HaulierId = dto.HaulierId
        };
        await _dbContext.Trailers.AddAsync(Trailer);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(Trailer.Id) ??  MapToDto(Trailer);
    }

    public async Task<TrailerDto?> UpdateAsync(Guid id, TrailerUpdateDto dto)
    {
        var Trailer = await _dbContext.Trailers.FirstOrDefaultAsync(t => t.Id == id);
        if (Trailer == null)
        {
            return null;
        }

        Trailer.PlateNumber = dto.PlateNumber;
        Trailer.Type = dto.Type;
        Trailer.BTM = dto.BTM;
        Trailer.UpdatedAt = dto.UpdatedAt;
        Trailer.HaulierId = dto.HaulierId;

        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(Trailer.Id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var trailer = await _dbContext.Trailers
            .FirstOrDefaultAsync(t => t.Id == id);
        if (trailer == null)
            return false;

        _dbContext.Trailers.Remove(trailer);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}