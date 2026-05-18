using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.Company;
using CLE_BackEnd.DTOs.User;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CLE_BackEnd.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;

    public static UserDto MapToDto(User user) => new()
    {
        UserId = user.UserId,
        FullName = user.FullName,
        CompanyCode = user.CompanyCode,
        CompanyName = user.CompanyName,
        Company = user.Company,
        Access = user.Access,
        AccessLevel = user.AccessLevel,
        EmailAddress = user.EmailAddress,
        ContactNumber = user.ContactNumber,
        Status = user.Status,
        UpdatedBy = user.UpdatedBy,
        CompanyAddress = user.Company?.Address ?? "No Address Found"
    };
    
    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var user = await _dbContext.Users
            .Include(u => u.Company)
            .ToListAsync();
        return user.Select(MapToDto);
    }

    public async Task<UserDto?> GetByIdAsync(string id)
    {
        var user = await _dbContext.Users
            .Include(u => u.Company)
            .FirstOrDefaultAsync(x => x.UserId == id);
        return user == null ? null : MapToDto(user);
    }

    public async Task<UserDto> CreateAsync(UserCreateDto dto)
    {
        var company = await _dbContext.Companies
            .FirstOrDefaultAsync(c => c.CompanyCode == dto.CompanyCode);
        
        string newCode;
        var companyInitial = !string.IsNullOrWhiteSpace(company?.CompanyName)
            ? company.CompanyName.Trim().First().ToString().ToUpper() 
            : "C";
        
        var cleanName = (dto.FullName ?? "").Replace(" ", "").Trim();
        var userInitial = cleanName.Length >= 2 
            ? cleanName.Substring(0, 2).ToUpper() 
            : cleanName.Length == 1 ? cleanName.ToUpper() + "US" : "US";
        
        string idPrefix = companyInitial + userInitial;
        var lastUser = await _dbContext.Users
            .Where(u => u.UserId.StartsWith(idPrefix))
            .OrderByDescending(u => u.UserId)
            .FirstOrDefaultAsync();
        
        if (lastUser == null)
        {
            newCode = idPrefix + "00001";
        }
        else
        {
            if (int.TryParse(lastUser.UserId.Substring(idPrefix.Length), out int lastNumber))
            {
                int nextNumber = lastNumber + 1;
                newCode = $"{idPrefix}{nextNumber.ToString("D5")}";
            }
            else
            {
                newCode = idPrefix + "00001";
            }
        }
        
        var user = new Models.User
        {
            UserId = newCode,
            Password = dto.Password,
            FullName = dto.FullName,
            CompanyCode = dto.CompanyCode,
            CompanyName = company?.CompanyName ?? "Unknown Company",
            Access = dto.Access,
            AccessLevel = dto.AccessLevel,
            EmailAddress = dto.EmailAddress,
            ContactNumber = dto.ContactNumber,
            Status = "Active",
        };
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(user.UserId) ?? MapToDto(user);
    }

    public async Task<UserDto?> UpdateAsync(string id, UserUpdateDto dto,  string currentUserId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
        if (user == null)
        {
            return null;
        }

        if (!string.IsNullOrEmpty(dto.NewPassword)){
            if (string.IsNullOrEmpty(dto.CurrentPassword))
            {
                throw new Exception("Current password is required to set a new password.");
            }

            bool isPasswordValid = dto.CurrentPassword == user.Password;

            if (!isPasswordValid)
            {
                throw new Exception("The current password you entered is incorrect.");
            }
            user.Password = dto.NewPassword;
        }

        var companyName = await _dbContext.Companies.Where(c => c.CompanyCode == dto.CompanyCode)
            .Select(c => c.CompanyName)
            .FirstOrDefaultAsync();
        
        user.FullName = dto.FullName;
        user.CompanyCode = dto.CompanyCode;
        user.CompanyName = companyName ?? "Unknown Company";
        user.Access = dto.Access;
        user.AccessLevel = dto.AccessLevel;
        user.EmailAddress = dto.EmailAddress;
        user.ContactNumber = dto.ContactNumber;
        user.Status = dto.Status;
        user.UpdatedBy = currentUserId;

        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(user.UserId);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.UserId == id);
        if (user == null)
            return false;
        
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}