using System.ComponentModel.DataAnnotations;

namespace CLE_BackEnd.DTOs.PrimeMover;

public class PrimeMoverDto
{
    public Guid Id { get; set; }
    public string PlateNumber { get; set; } = string.Empty;
    public string PMCode { get; set; } = string.Empty;
    public string? BTM { get; set; }
    public string? BGK { get; set; }
    public string? DefaultDriver { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string HaulierId { get; set; } = string.Empty;
}

public class PrimeMoverCreateDto
{
    [Required]
    public string PlateNumber { get; set; } = string.Empty;
    
    [Required]
    public string PMCode { get; set; } = string.Empty;
    
    public string? BTM { get; set; }
    
    public string? BGK { get; set; }
    
    public string? DefaultDriver { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    [Required]
    public string HaulierId { get; set; } = string.Empty;
}

public class PrimeMoverUpdateDto
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string PlateNumber { get; set; } = string.Empty;
    
    [Required]
    public string PMCode { get; set; } = string.Empty;
    
    public string? BTM { get; set; }
    
    public string? BGK { get; set; }
    
    public string? DefaultDriver { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    [Required]
    public string HaulierId { get; set; } = string.Empty;
}