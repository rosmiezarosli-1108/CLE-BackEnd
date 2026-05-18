using CLE_BackEnd.Models;
using CLE_BackEnd.Data;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _dbContext;
    
    public AuthService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<AuthResult> AuthenticateAsync(string userId, string password, string region, string access)
    {
        var user = await _dbContext.Users
            .Include(u => u.Company)
            .FirstOrDefaultAsync(u => u.UserId == userId && u.Password == password);
        
        if (user == null)
            return new AuthResult { ErrorMessage = "Invalid User ID or Password." };

        bool isRegionRegistered = user.Company.Region.Any(r => 
            r.SystemName == access && r.RegionCode == region);
        
        if (!isRegionRegistered)
            return new AuthResult { ErrorMessage = $"Your account is not registered for the {region} region." };

        if (!user.Access.Contains(access))
            return new AuthResult { ErrorMessage = $"You do not have permission to access the {access} system." };
        
        return new AuthResult { User = user };
    }
}