using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLE_BackEnd.Models;

public class TimeSlot
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
    public string DepotId { get; set; } = string.Empty;
    
    [ForeignKey("DepotId")]
    public virtual Company? Depot { get; set; }
}
