using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLE_BackEnd.Models;

public class Company
{
    [Key]
    [Column(TypeName = "varchar(6)")]
    public string CompanyCode { get; set; } = string.Empty;
    
    [Required]
    public string CompanyName { get; set; } = string.Empty;
    
    [Required]
    public string SSMNo { get; set; } = string.Empty;
    
    [Required]
    public string SSTNo { get; set; } = string.Empty;
    
    [Required]
    public string Role { get; set; } = string.Empty;
    
    [Required]
    public List<SystemRegion> Region { get; set; } = new List<SystemRegion>();
    
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
    public string EmailAddress { get; set; } = string.Empty;
    
    public string? CCEmailAddress { get; set; }
    
    [Required]
    public string CLEKmailNotification { get; set; } = string.Empty;

    [Required] 
    public string LogoPath { get; set; } = string.Empty;

}

public class SystemRegion
{
    public string SystemName { get; set; } = string.Empty;
    public string RegionCode { get; set; } = string.Empty;
}