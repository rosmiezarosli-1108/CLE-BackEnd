using CLE_BackEnd.DTOs.Trailer;

namespace CLE_BackEnd.Services;

public interface ITrailerService
{
    Task<IEnumerable<TrailerDto>> GetAllAsync();
    Task<TrailerDto?> GetByIdAsync(Guid id);
    Task<TrailerDto> CreateAsync(TrailerCreateDto dto);
    Task<TrailerDto?> UpdateAsync(Guid id, TrailerUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}