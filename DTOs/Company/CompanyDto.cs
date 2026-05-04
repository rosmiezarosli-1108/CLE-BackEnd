using System.ComponentModel.DataAnnotations;

namespace CLE_BackEnd.DTOs.Company;

public class CompanyDto
{
    public string CompanyCode { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string SSMNo { get; set; } = string.Empty;
    public string SSTNo { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string ManagerName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string TelephoneNumber { get; set; } = string.Empty;
    public string? FaxNumber { get; set; }
    public string PICName { get; set; } = string.Empty;
    public string HandphoneNumber { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string? CCEmailAddress { get; set; }
    public string CLEKmailNotification { get; set; } = string.Empty; 
    public string LogoPath { get; set; } = string.Empty;
}

public class CompanyCreateDto
{
    [Required]
    public string CompanyName { get; set; } = string.Empty;

    [Required]
    public string SSMNo { get; set; } = string.Empty;
    
    [Required]
    public string SSTNo { get; set; } = string.Empty;
    
    [Required]
    public string ManagerName { get; set; } = string.Empty;
    
    [Required]
    public string Address { get; set; } = string.Empty;
    
    [Required]
    public string TelephoneNumber { get; set; } = string.Empty;
    
    public string? FaxNumber { get; set; }
    
    [Required]
    public string PICName { get; set; } = string.Empty;
    
    [Required]
    public string HandphoneNumber { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;
    
    public string? CCEmailAddress { get; set; }
    
    [Required]
    public string CLEKmailNotification { get; set; } = string.Empty;
    
    [Required]
    public string Region { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty;
    
    [Required]
    public string LogoPath { get; set; } = string.Empty;
}

public class CompanyUpdateDto : CompanyCreateDto
{
    [Required]
    public string CompanyCode { get; set; } = string.Empty;
}