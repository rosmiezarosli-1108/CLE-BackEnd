using System.ComponentModel.DataAnnotations;

namespace CLE_BackEnd.Models;

public class Notification
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string ReceiverId { get; set; }
    
    [Required]
    public string Message { get; set; } = string.Empty;
    
    public string? ROTNumber { get; set; }
    
    public int? ContainerId { get; set; }
    
    public bool IsRead { get; set; } = false;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}