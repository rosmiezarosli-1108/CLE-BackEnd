using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLE_BackEnd.Models;

public class AleAssignedHaulier
{
    [Key] 
    public int Id { get; set; }

    [Required] 
    public Guid DriverId { get; set; }
    
    [ForeignKey("DriverId")]
    public virtual Driver? Driver { get; set; }

    [Required] 
    public Guid PMId { get; set; }
    
    [ForeignKey("PMId")]
    public virtual PrimeMover? PrimeMover { get; set; }

    [Required] 
    public Guid TimeSlotId { get; set; }
    
    [ForeignKey("TimeSlotId")]
    public virtual TimeSlot? TimeSlot { get; set; }
    
    public Guid? TrailerId { get; set; }
    
    [ForeignKey("TrailerId")]
    public virtual Trailer? Trailer { get; set; }

    [Required] 
    public int ContainerId { get; set; }

    [ForeignKey("ContainerId")] 
    public virtual AleContainer AleContainer { get; set; } = null!;

    [Required] 
    public string ROTNumber { get; set; } = string.Empty;

    [ForeignKey("ROTNumber")] 
    public virtual AleBooking AleBooking { get; set; } = null!;
    
    [Required]
    public string HaulierId { get; set; } = string.Empty;
}