using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLE_BackEnd.Models;

public class Container
{
    [Key]
    public int ContainerId { get; set; }
    
    public string? ContainerNumber { get; set; }
    
    [Required]
    public string ContainerType { get; set; } = string.Empty;
    
    [Required]
    public string ContainerSize { get; set; } = string.Empty;

    public string? VGM { get; set; }
    
    public string? TrailerType { get; set; } = string.Empty;
    
    [Required]
    public string ConsigneeId { get; set; } = string.Empty;
    
    [ForeignKey("ConsigneeId")]
    public virtual Company? ConsigneeCompany { get; set; }
    
    [Required]
    public string HaulierId { get; set; } = string.Empty; 
    
    [ForeignKey("HaulierId")]
    public virtual Company? HaulierCompany { get; set; }

    public string? DepotId { get; set; }
    
    [ForeignKey("DepotId")]
    public virtual Company? DepotCompany { get; set; }
    
    public string? PortId { get; set; }
    
    [ForeignKey("PortId")]
    public virtual Company? PortCompany { get; set; }

    [Required]
    public List<ContainerAddress> ToAddress { get; set; } = new List<ContainerAddress>();
    
    [Required(ErrorMessage = "ROT Date is required")]
    public DateOnly ROTDate { get; set; }
    
    [Required]
    public string ROTNumber { get; set; } = string.Empty;

    [ForeignKey("ROTNumber")] 
    public virtual Booking Booking { get; set; } = null!;
    
    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = string.Empty;
    
    [Required]
    public DateTime AssignedTime { get; set; }
    
    public DateTime? EnrouteTime { get; set; }
    
    public DateTime? AcceptedTime { get; set; }
    
    public DateTime? GatedInTime { get; set; }
    
    public DateTime? GatedOutTime { get; set; }
    
    public DateTime? DeliveredTime { get; set; }
    
    public DateTime? RFCTime { get; set; }
    
    public DateTime? RejectedTime { get; set; }
    
    public DateTime? DeletedTime { get; set; }
    
    public DateTime? RTAssignedTime { get; set; }
    
    public DateTime? RTEnrouteTime { get; set; }
    
    public DateTime? RTAcceptedTime { get; set; }
    
    public DateTime? RTGatedInTime { get; set; }
    
    public DateTime? RTGatedOutTime { get; set; }
    
    public DateTime? RTDeliveredTime { get; set; }
    
    public DateTime? RTRFCTime { get; set; }
    
    public string? TimeStatus { get; set; }
    
    public int? TurnAroundTime { get; set; }
    
    public double? DGCRate { get; set; }
    
    public bool DGCReductionEligibility { get; set; }
    
    public double? DGCReduction { get; set; }
    
    public string? DeletedRemarks { get; set; }
}