using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLE_BackEnd.Models;

public class Booking
{
    [Key]
    public string ROTNumber { get; set; } = string.Empty;
    
    [Required]
    public string BLOrBookingNumber { get; set; } = string.Empty;
    
    [Required]
    public string HouseBLNumber { get; set; } = string.Empty;
    
    [Required]
    public string MovementType { get; set; } = string.Empty;
    
    [Required]
    public string SCN { get; set; } = string.Empty;
    
    public string? TripType { get; set; }
    
    [Required]
    public string PortLocation { get; set; } = string.Empty;
    
    [ForeignKey("PortLocation")]
    public virtual Company? PortLocationCompany { get; set; }
    
    [Required(ErrorMessage = "ETA is required")]
    public DateOnly? ETA { get; set; }
    
    public string? SealNumber { get; set; }
    
    public string? ForwardingRemarks { get; set; }
    
    public string? HaulierRemarks { get; set; }
    
    public string? DepotRemarks { get; set; }
    
    [Required]
    public string ForwardingId { get; set; } = string.Empty;
    
    [ForeignKey("ForwardingId")]
    public virtual Company? ForwardingCompany { get; set; }
   
    [Required]
    public string ShippingAgentId { get; set; } = string.Empty;
    
    [ForeignKey("ShippingAgentId")]
    public virtual Company? ShippingAgentCompany { get; set; }
    
    [Required]
    public string BillingParty { get; set; } = string.Empty;
    
    public string? CustomFormNo { get; set; }
    
    public string? CustomReceiptNo { get; set; }
    
    public string? DICNumber { get; set; }
    
    public string? ZBNumber { get; set; }
    
    public int ContainerQuantity { get; set; }
    
}