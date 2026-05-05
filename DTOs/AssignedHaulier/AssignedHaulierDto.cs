using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLE_BackEnd.DTOs.AssignedHaulier;

public class AssignedHaulierDto
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
    public string ROTNumber { get; set; } = string.Empty;
    public string HaulierId { get; set; } = string.Empty;
    public string HaulierName { get; set; } = string.Empty;
}

public class AssignedHaulierCreateDto
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
}

public class AssignedHaulierUpdateDto : AssignedHaulierCreateDto
{
    [Required]
    public int Id { get; set; }
}