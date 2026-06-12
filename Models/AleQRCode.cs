using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLE_BackEnd.Models;

public class AleQRCode
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
    
    [ForeignKey("TerminalId")]
    public virtual Company? Terminal { get; set; }
    
    public string? ScannedById { get; set; }
    
    [ForeignKey("ScannedById")]
    public virtual User? ScannedBy { get; set; }
    
    public int? ContainerId { get; set; }
    
    [ForeignKey("ContainerId")]
    public virtual Container? Container { get; set; }
}