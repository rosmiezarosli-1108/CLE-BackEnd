using CLE_BackEnd.DTOs.PrimeMover;

namespace CLE_BackEnd.Services;

public interface IPrimeMoverService
{
    Task<IEnumerable<PrimeMoverDto>> GetAllAsync();
    Task<PrimeMoverDto?> GetByIdAsync(Guid id);
    Task<PrimeMoverDto> CreateAsync(PrimeMoverCreateDto dto);
    Task<PrimeMoverDto?> UpdateAsync(Guid id, PrimeMoverUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}