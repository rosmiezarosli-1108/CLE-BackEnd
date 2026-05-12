using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLE_BackEnd.Models;

public class AleContainer
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

    public string? TerminalId { get; set; }
    
    [ForeignKey("TerminalId")]
    public virtual Company? TerminalCompany { get; set; }

    [Required]
    public List<AleContainerAddress> ToAddress { get; set; } = new List<AleContainerAddress>();
    
    [Required(ErrorMessage = "ROT Date is required")]
    public DateOnly ROTDate { get; set; }
    
    [Required]
    public string ROTNumber { get; set; } = string.Empty;

    [ForeignKey("ROTNumber")] 
    public virtual AleBooking AleBooking { get; set; } = null!;
    
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
    
    public string? RejectedRemarks { get; set; }

    public virtual List<AleContainerAudit> UpdateHistory { get; set; } = new List<AleContainerAudit>();
    
    public string? ReceivedBy { get; set; }
    
    public DateTime? ApprovedAKPSTime { get; set; }
    
    public DateTime? ApprovedCustomsTime { get; set; }
    
    public DateTime? ApprovedBothTime { get; set; }
    
    public string? EditRemarks { get; set; }
    
    public string? PackageQuantity { get; set; }

    public string? VolumeMetricWeight { get; set; }
}
