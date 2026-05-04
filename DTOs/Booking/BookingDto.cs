using System.ComponentModel.DataAnnotations;

namespace CLE_BackEnd.DTOs.Booking;

public class BookingDto
{
    public string ROTNumber { get; set; } = string.Empty;
    public string BLOrBookingNumber { get; set; } = string.Empty;
    public string HouseBLNumber { get; set; } = string.Empty;
    public string MovementType { get; set; } = string.Empty;
    public string? TripType { get; set; }
    public string SCN { get; set; } = string.Empty;
    public string PortLocation { get; set; } = string.Empty;
    public Models.Company? Port { get; set; }
    public string PortLocationName {get; set; } = string.Empty;
    public DateOnly ETA { get; set; }
    public string? SealNumber { get; set; }
    public string? ForwardingRemarks { get; set; }
    public string? HaulierRemarks { get; set; }
    public string? DepotRemarks { get; set; }
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

public class BookingCreateDto
{
    [Required]
    public string ROTNumber { get; set; } = string.Empty;
    
    [Required]
    public string BLOrBookingNumber { get; set; } = string.Empty;
    
    [Required]
    public string HouseBLNumber { get; set; } = string.Empty;

    [Required]
    public string MovementType { get; set; } = string.Empty;

    public string? TripType { get; set; }
    
    [Required]
    public string SCN { get; set; } = string.Empty;

    [Required]
    public string PortLocation { get; set; } = string.Empty;

    [Required(ErrorMessage = "ETA is required")]
    public DateOnly ETA { get; set; }
    public string? SealNumber { get; set; }
    public string? ForwardingRemarks { get; set; }
    public string? HaulierRemarks { get; set; }
    public string? DepotRemarks { get; set; }

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

public class BookingUpdateDto : BookingCreateDto
{
    
}