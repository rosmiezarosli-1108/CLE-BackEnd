using CLE_BackEnd.DTOs.Company;
using CLE_BackEnd.DTOs.User;

namespace CLE_BackEnd.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(string id);
    Task<UserDto> CreateAsync(UserCreateDto dto);
    Task<UserDto?> UpdateAsync(string id, UserUpdateDto dto, string currentUserId);
    Task<bool> DeleteAsync(string id);
}