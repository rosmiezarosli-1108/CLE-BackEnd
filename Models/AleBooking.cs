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
    
    [Required]
    public string MovementType { get; set; } = string.Empty;
    
    [Required]
    public string SCN { get; set; } = string.Empty;
    
    public string? TripType { get; set; }
    
    [Required]
    public string TerminalLocation { get; set; } = string.Empty;
    
    [ForeignKey("TerminalLocation")]
    public virtual Company? TerminalCompany { get; set; }
    
    [Required(ErrorMessage = "ETA is required")]
    public DateOnly? ETA { get; set; }
    
    public string? SealNumber { get; set; }
    
    public string? ForwardingRemarks { get; set; }
    
    public string? HaulierRemarks { get; set; }
    
    public string? TerminalRemarks { get; set; }
    
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