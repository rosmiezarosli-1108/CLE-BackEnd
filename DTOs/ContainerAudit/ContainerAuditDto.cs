using System.ComponentModel.DataAnnotations;

namespace CLE_BackEnd.DTOs.ContainerAudit;

public class ContainerAuditDto
{
    public int AuditId { get; set; }
    public int ContainerId { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public DateTime UpdatedTime { get; set; } = DateTime.Now;
    public string? Action { get; set; }
    public string? Changes { get; set; }
}

public class ContainerAuditCreateDto
{
    public int ContainerId { get; set; }
    
    [Required]
    public string UpdatedBy { get; set; } = string.Empty;

    [Required]
    public DateTime UpdatedTime { get; set; } = DateTime.Now;

    public string? Action { get; set; }
    
    public string? Changes { get; set; }
}

public class ContainerAuditUpdateDto
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
