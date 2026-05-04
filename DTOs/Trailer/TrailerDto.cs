using System.ComponentModel.DataAnnotations;

namespace CLE_BackEnd.DTOs.Trailer;

public class TrailerDto
{
    public Guid Id { get; set; }
    public string PlateNumber { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? BTM { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class TrailerCreateDto
{
    [Required]
    public string PlateNumber { get; set; } = string.Empty;
    
    [Required]
    public string Type { get; set; } = string.Empty;
    
    public string? BTM { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}

public class TrailerUpdateDto
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