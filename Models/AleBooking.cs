using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLE_BackEnd.Models;

public class AleBooking
{
    [Key]
    public string ROTNumber { get; set; } = string.Empty;
    
    [Required]
    public string AWBNumber { get; set; } = string.Empty;
    
    [Required]
    public string HouseAWBNumber { get; set; } = string.Empty;
    
    public string MovementType { get; set; } = string.Empty;
    
    [Required]
    public string FlightNumber { get; set; } = string.Empty;
    
    public string? TripType { get; set; }
    
    public string TerminalLocation { get; set; } = string.Empty;
    
    [ForeignKey("TerminalLocation")]
    public virtual Company? TerminalCompany { get; set; }
    
    public DateOnly? ETA { get; set; }
    
    public string? SealNumber { get; set; }
    
    public string? ForwardingRemarks { get; set; }
    
    public string? HaulierRemarks { get; set; }
    
    public string? TerminalRemarks { get; set; }
    
    public string ForwardingId { get; set; } = string.Empty;
    
    [ForeignKey("ForwardingId")]
    public virtual Company? ForwardingCompany { get; set; }
   
    public string AirlineId { get; set; } = string.Empty;
    
    [ForeignKey("AirlineId")]
    public virtual Company? AirlineCompany { get; set; }
    
    public string BillingParty { get; set; } = string.Empty;
    
    public string? CustomFormType { get; set; }
    
    public string? CustomFormNo { get; set; }
    
    public string? CustomReceiptNo { get; set; }
    
    public string? DICNumber { get; set; }
    
    public string? ZBNumber { get; set; }
    
    public int TruckQuantity { get; set; }
    
    public string CarrierReferenceNumber { get; set; } = string.Empty;
    
    public int? TotalPackageQuantity { get; set; }
    
    public double? Weight { get; set; }
    
    public string? ConsigneeId { get; set; }
    
    [ForeignKey("ConsigneeId")]
    public virtual Company? ConsigneeCompany { get; set; }
    
    public string? SSMNumber { get; set; }
    
    public string? ExternalConsigneeName { get; set; }
    
    public string? ExternalConsigneeAddress { get; set; }
    
    public string? ExternalConsigneeContact { get; set; }
    
    public string? Size { get; set; }
    
    public string? BookingAgentId { get; set; }
    
    [ForeignKey("BookingAgentId")]
    public virtual Company? BookingAgentCompany { get; set; }
}