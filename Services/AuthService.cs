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
        // 1. Fetch the user directly matching the UserId and Password
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.UserId == userId && u.Password == password);
        
        if (user == null)
            return new AuthResult { ErrorMessage = "Invalid User ID or Password." };

        // 2. Fetch the corresponding Company manually using the CompanyCode string to bypass unlinked entity references
        var company = await _dbContext.Companies
            .FirstOrDefaultAsync(c => c.CompanyCode == user.CompanyCode);

        if (company == null || company.Region == null)
        {
            return new AuthResult { ErrorMessage = "Your assigned company or regional settings could not be verified." };
        }

        // 3. Validate if the system region exists for this specific company
        bool isRegionRegistered = company.Region.Any(r => 
            r.SystemName == access && r.RegionCode == region);
        
        if (!isRegionRegistered)
            return new AuthResult { ErrorMessage = $"Your account is not registered for the {region} region." };

        // 4. Validate system level access permissions
        if (string.IsNullOrEmpty(user.Access) || !user.Access.Contains(access))
            return new AuthResult { ErrorMessage = $"You do not have permission to access the {access} system." };
        
        // Temporarily link the object for the controller payload return requirement
        user.Company = company;
        
        return new AuthResult { User = user };
    }
}