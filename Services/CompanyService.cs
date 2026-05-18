using CLE_BackEnd.Data;
using CLE_BackEnd.DTOs.Company;
using CLE_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CLE_BackEnd.Services;

public class CompanyService : ICompanyService
{
    private readonly ApplicationDbContext _dbContext;

    public static CompanyDto MapToDto(Company company) => new()
    {
        CompanyCode = company.CompanyCode,
        CompanyName = company.CompanyName,
        SSMNo = company.SSMNo,
        SSTNo = company.SSTNo,
        ManagerName = company.ManagerName,
        Address = company.Address,
        TelephoneNumber = company.TelephoneNumber,
        FaxNumber = company.FaxNumber,
        PICName = company.PICName,
        HandphoneNumber = company.HandphoneNumber,
        EmailAddress = company.EmailAddress,
        CCEmailAddress = company.CCEmailAddress,
        CLEKmailNotification = company.CLEKmailNotification,
        Region = company.Region.Select(r => new SystemRegionDto
        {
            SystemName = r.SystemName,
            RegionCode = r.RegionCode
        }).ToList(),
        Role = company.Role,
        LogoPath = company.LogoPath,
    };
    
    public CompanyService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CompanyDto>> GetAllAsync()
    {
        var companies = await _dbContext.Companies.ToListAsync();
        return companies.Select(MapToDto);
    }

    public async Task<CompanyDto?> GetByIdAsync(string id)
    {
        var company = await _dbContext.Companies.FirstOrDefaultAsync(x => x.CompanyCode == id);
        return company == null? null : MapToDto(company);
    }

    public async Task<CompanyDto> CreateAsync(CompanyCreateDto dto)
    {
        string initial = dto.CompanyName.Substring(0, 1).ToUpper();
        var lastCompany = await _dbContext.Companies
            .Where(c => c.CompanyCode.StartsWith(initial))
            .OrderByDescending(c => c.CompanyCode)
            .FirstOrDefaultAsync();
        string newCode;
        
        if (lastCompany == null)
        {
            newCode = $"{initial}00001";
        }
        else
        {
            if (int.TryParse(lastCompany.CompanyCode.Substring(1), out int lastNumber))
            {
                int nextNumber = lastNumber + 1;
                newCode = $"{initial}{nextNumber.ToString("D5")}";
            }
            else
            {
                newCode = $"{initial}00001";
            }
        }

        var company = new Models.Company
        {
            CompanyCode = newCode,
            CompanyName = dto.CompanyName,
            SSMNo = dto.SSMNo,
            SSTNo = dto.SSTNo,
            ManagerName = dto.ManagerName,
            Address = dto.Address,
            TelephoneNumber = dto.TelephoneNumber,
            FaxNumber = dto.FaxNumber,
            PICName = dto.PICName,
            HandphoneNumber = dto.HandphoneNumber,
            EmailAddress = dto.EmailAddress,
            CCEmailAddress = dto.CCEmailAddress,
            CLEKmailNotification = dto.CLEKmailNotification,
            Region = dto.Region.Select(r => new SystemRegion
            {
                SystemName = r.SystemName,
                RegionCode = r.RegionCode
            }).ToList(),
            Role = dto.Role,
            LogoPath = dto.LogoPath
        };
        await _dbContext.Companies.AddAsync(company);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(company.CompanyCode) ?? MapToDto(company);
    }

    public async Task<CompanyDto?> UpdateAsync(string id, CompanyUpdateDto dto)
    {
        var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.CompanyCode == id);
        if (company == null)
        {
            return null;
        }

        company.CompanyName = dto.CompanyName;
        company.SSMNo = dto.SSMNo;
        company.SSTNo = dto.SSTNo;
        company.ManagerName = dto.ManagerName;
        company.Address = dto.Address;
        company.TelephoneNumber = dto.TelephoneNumber;
        company.FaxNumber = dto.FaxNumber;
        company.PICName = dto.PICName;
        company.HandphoneNumber = dto.HandphoneNumber;
        company.EmailAddress = dto.EmailAddress;
        company.CCEmailAddress = dto.CCEmailAddress;
        company.CLEKmailNotification = dto.CLEKmailNotification;
        company.Region = dto.Region.Select(r => new SystemRegion
        {
            SystemName = r.SystemName,
            RegionCode = r.RegionCode
        }).ToList();
        company.Role = dto.Role;
        company.LogoPath = dto.LogoPath;

        // update company name together in the User table
        var userCompany = await _dbContext.Users
            .Where(u => u.CompanyCode == company.CompanyCode)
            .ToListAsync();
        foreach (var user in userCompany)
        {
            user.CompanyName = dto.CompanyName; 
        }

        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(company.CompanyCode);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var company = await _dbContext.Companies
            .FirstOrDefaultAsync(c => c.CompanyCode == id);
        if (company == null)
            return false;

        //to remove the users of the company
        var companyUsers = await _dbContext.Users
            .Where(u => u.CompanyCode == id).ToListAsync();
        if (companyUsers.Any())
        {
            _dbContext.Users.RemoveRange(companyUsers);
        }

        _dbContext.Companies.Remove(company);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
