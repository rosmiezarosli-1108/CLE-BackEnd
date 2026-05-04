using System.ComponentModel.DataAnnotations;

namespace CLE_BackEnd.Models;

public class Driver
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