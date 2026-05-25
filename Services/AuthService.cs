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

        // 2. Fetch the corresponding Company manually using the CompanyCode string
        var company = await _dbContext.Companies
            .FirstOrDefaultAsync(c => c.CompanyCode == user.CompanyCode);

        if (company == null)
            return new AuthResult { ErrorMessage = $"Company profile ({user.CompanyCode}) could not be found." };

        // 3. Fallback check: If the Region collection didn't load or is empty
        if (company.Region == null || !company.Region.Any())
        {
            // If the database list is empty, perform a primitive fallback validation to prevent a crash
            if (region != "PEN")
            {
                return new AuthResult { ErrorMessage = $"Your account is not registered for the {region} region." };
            }
        }
        else
        {
            // If the collection is present, evaluate it using safe item checking
            bool isRegionRegistered = company.Region.Any(r => 
                r != null && r.SystemName == access && r.RegionCode == region);
            
            if (!isRegionRegistered)
                return new AuthResult { ErrorMessage = $"Your account is not registered for the {region} region." };
        }

        // 4. Validate system level access permissions safely
        if (string.IsNullOrEmpty(user.Access) || !user.Access.Contains(access))
            return new AuthResult { ErrorMessage = $"You do not have permission to access the {access} system." };
        
        // Ensure properties are assigned to prevent NullReferenceExceptions in the controller payload return
        user.Company = company;
        
        return new AuthResult { User = user };
    }
}