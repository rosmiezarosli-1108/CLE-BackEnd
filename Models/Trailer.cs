using System.ComponentModel.DataAnnotations;

namespace CLE_BackEnd.Models;

public class Trailer
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string PlateNumber { get; set; } = string.Empty;
    
    [Required]
    public string Type { get; set; } = string.Empty;
    
    public string? BTM { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}