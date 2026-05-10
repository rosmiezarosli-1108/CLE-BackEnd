using System.ComponentModel.DataAnnotations;
using CLE_BackEnd.Models;

namespace CLE_BackEnd.DTOs.AleBooking;

public class AleBookingDto
{
    public string ROTNumber { get; set; } = string.Empty;
    public string AWBNumber { get; set; } = string.Empty;
    public string HouseAWBNumber { get; set; } = string.Empty;
    public string MovementType { get; set; } = string.Empty;
    public string? TripType { get; set; }
    public string SCN { get; set; } = string.Empty;
    public string TerminalLocation { get; set; } = string.Empty;
    public Models.Company? Terminal { get; set; }
    public string TerminalLocationName {get; set; } = string.Empty;
    public DateOnly ETA { get; set; }
    public string? SealNumber { get; set; }
    public string? ForwardingRemarks { get; set; }
    public string? HaulierRemarks { get; set; }
    public string? TerminalRemarks { get; set; }
    public string ForwardingId {get; set; } = string.Empty;
    public Models.Company? Forwarding { get; set; }
    public string ForwardingName {get; set; } = string.Empty;
    public string HaulierName {get; set; } = string.Empty;
    public string ShippingAgentId { get; set; } = string.Empty;
    public Models.Company? ShippingAgent { get; set; }
    public string ShippingAgentName { get; set; } = string.Empty;
    public string BillingParty { get; set; } = string.Empty;
    public string? CustomFormNo { get; set; }
    public string? CustomReceiptNo { get; set; }
    public string? DICNumber { get; set; }
    public string? ZBNumber { get; set; }
    public int ContainerQuantity { get; set; }
}

public class AleBookingCreateDto
{
    [Required]
    public string ROTNumber { get; set; } = string.Empty;
    
    [Required]
    public string AWBNumber { get; set; } = string.Empty;
    
    [Required]
    public string HouseAWBNumber { get; set; } = string.Empty;

    [Required]
    public string MovementType { get; set; } = string.Empty;

    public string? TripType { get; set; }
    
    [Required]
    public string SCN { get; set; } = string.Empty;

    [Required]
    public string TerminalLocation { get; set; } = string.Empty;

    [Required(ErrorMessage = "ETA is required")]
    public DateOnly ETA { get; set; }
    public string? SealNumber { get; set; }
    public string? ForwardingRemarks { get; set; }
    public string? HaulierRemarks { get; set; }
    public string? TerminalRemarks { get; set; }

    [Required]
    public string ForwardingId { get; set; } = string.Empty;

    [Required]
    public string ShippingAgentId { get; set; } = string.Empty;

    [Required]
    public string BillingParty { get; set; } = string.Empty;
    
    public string? CustomFormNo { get; set; }
    public string? CustomReceiptNo { get; set; }
    public string? DICNumber { get; set; }
    public string? ZBNumber { get; set; }
    public int ContainerQuantity { get; set; }
}

public class AleBookingUpdateDto : AleBookingCreateDto
{

}