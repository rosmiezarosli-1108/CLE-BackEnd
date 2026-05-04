using System.ComponentModel.DataAnnotations;

namespace CLE_BackEnd.DTOs.Driver;

public class DriverDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ICNumber { get; set; } = string.Empty;
    public string EmailAddress { get; set; } =  string.Empty;
    public string? MobileNumber { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class DriverCreateDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string ICNumber { get; set; } = string.Empty;
    
    [Required]
    public string EmailAddress { get; set; } =  string.Empty;
    
    public string? MobileNumber { get; set; }
    
    public DateTime? UpdatedAt { get; set; }   
}

public class DriverUpdateDto
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string ICNumber { get; set; } = string.Empty;
    
    [Required]
    public string EmailAddress { get; set; } =  string.Empty;
    
    public string? MobileNumber { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}