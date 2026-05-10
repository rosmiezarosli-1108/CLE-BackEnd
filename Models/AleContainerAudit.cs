using System.ComponentModel.DataAnnotations;

namespace CLE_BackEnd.Models;

public class AleContainerAudit
{
    [Key]
    public int AuditId { get; set; }

    public int ContainerId { get; set; }
    
    [Required]
    public string UpdatedBy { get; set; } = string.Empty;

    [Required]
    public DateTime UpdatedTime { get; set; } = DateTime.Now;

    public string? Action { get; set; }
    
    public string? Changes { get; set; }
}