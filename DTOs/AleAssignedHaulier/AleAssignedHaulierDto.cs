using System.ComponentModel.DataAnnotations;

namespace CLE_BackEnd.DTOs.AleAssignedHaulier;

public class AleAssignedHaulierDto
{
    public int Id { get; set; }
    public Guid DriverId { get; set; }
    public Models.Driver? Driver { get; set; }
    public Guid PMId { get; set; }
    public Models.PrimeMover? PrimeMover { get; set; }
    public Guid TimeSlotId { get; set; }
    public Models.TimeSlot? TimeSlot { get; set; }
    public Guid? TrailerId { get; set; }
    public Models.Trailer? Trailer { get; set; }
    public int ContainerId { get; set; }
    public Models.AleContainer? AleContainer { get; set; }
    public string ROTNumber { get; set; } = string.Empty;
    public Models.AleBooking? AleBooking { get; set; }
    public string HaulierId { get; set; } = string.Empty;
    public string HaulierName { get; set; } = string.Empty;
    public string PassNumber { get; set; }
    public TimeOnly? ConsigneeTimeSlot { get; set; }
}

public class AleAssignedHaulierCreateDto
{

    [Required] 
    public Guid DriverId { get; set; }

    [Required] 
    public Guid PMId { get; set; }

    [Required] 
    public Guid TimeSlotId { get; set; }
    
    public Guid? TrailerId { get; set; }

    [Required] 
    public int ContainerId { get; set; }
    
    [Required] 
    public string ROTNumber { get; set; } = string.Empty;

    [Required]
    public string HaulierId { get; set; } = string.Empty;
    
    public string PassNumber { get; set; }
    
    public TimeOnly? ConsigneeTimeSlot { get; set; }
}

public class AleAssignedHaulierUpdateDto : AleAssignedHaulierCreateDto
{
    [Required]
    public int Id { get; set; }
}