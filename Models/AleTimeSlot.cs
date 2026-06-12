using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLE_BackEnd.Models;

public class AleTimeSlot
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public DateOnly Date { get; set; }
    
    [Required]
    public string Time { get; set; } = string.Empty;
    
    public int? PickUpTotalSlot { get; set; }
    
    public int? DropOffTotalSlot { get; set; }
    
    [Required]
    public string TerminalId { get; set; } = string.Empty;
    
    [ForeignKey("TerminalId")]
    public virtual Company? Terminal { get; set; }
    
    public string? ChangeRemarks { get; set; }

    public bool IsCancelled { get; set; } = false;
    
    public virtual ICollection<AleAssignedHaulier> AssignedHauliers { get; set; } = new List<AleAssignedHaulier>();
}