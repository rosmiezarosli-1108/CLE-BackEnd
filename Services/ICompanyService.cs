using CLE_BackEnd.DTOs.Company;

namespace CLE_BackEnd.Services;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetAllAsync();
    Task<CompanyDto?> GetByIdAsync(string id);
    Task<CompanyDto> CreateAsync(CompanyCreateDto dto);
    Task<CompanyDto?> UpdateAsync(string id, CompanyUpdateDto dto);
    Task<bool> DeleteAsync(string id);
}