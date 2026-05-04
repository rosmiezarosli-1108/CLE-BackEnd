using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLE_BackEnd.Models;

public class AssignedHaulier
{
    [Key] 
    public int Id { get; set; }

    [Required] 
    public string DriverName { get; set; } = string.Empty;

    [Required] 
    public string PMNumber { get; set; } = string.Empty;

    [Required] 
    public string TimeSlot { get; set; } = string.Empty;

    [Required] 
    public int ContainerId { get; set; }

    [ForeignKey("ContainerId")] 
    public virtual Container Container { get; set; } = null!;

    [Required] 
    public string ROTNumber { get; set; } = string.Empty;

    [ForeignKey("ROTNumber")] 
    public virtual Booking Booking { get; set; } = null!;
    
    [Required]
    public string HaulierId { get; set; } = string.Empty;
}