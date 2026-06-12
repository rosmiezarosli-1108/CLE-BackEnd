using System.ComponentModel.DataAnnotations;

namespace CLE_BackEnd.DTOs.AleQRCodeDto;

public class AleQRCodeDto
{
    public Guid Id { get; set; }
    public string QRCode { get; set; } =  string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? VerifiedAt { get; set; }
    public string? TerminalId { get; set; }
    public Models.Company? Terminal { get; set; }
    public string? ScannedById { get; set; }
    public Models.User? ScannedBy { get; set; }
    public int? ContainerId { get; set; }
    public Models.Container? Container { get; set; }
}

public class AleQRCodeCreateDto
{
    [Required]
    public string TerminalId { get; set; } =  string.Empty;
    
    public int? ContainerId { get; set; }
}

public class AleQRCodeUpdateDto
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string QRCode { get; set; } =  string.Empty;
    
    [Required]
    public string Status { get; set; } = string.Empty;
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? VerifiedAt { get; set; }
    
    public string? TerminalId { get; set; }
    
    public string? ScannedById { get; set; }
    
    public int? ContainerId { get; set; }
}

public class AleQRCodeVerifyRequestDto
{
    [Required]
    public string QRCode { get; set; } = string.Empty;
    
    [Required]
    public string ScannedById { get; set; } = string.Empty;
}

public class AleQRCodeVerificationResultDto
{
    public bool Success { get; set; }
    public string? Status { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public string? TerminalName { get; set; } 
    public string? Message { get; set; }
}