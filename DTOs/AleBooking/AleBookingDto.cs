using System.ComponentModel.DataAnnotations;
using CLE_BackEnd.Models;

namespace CLE_BackEnd.DTOs.AleBooking;

public class AleBookingDto
{
    public string ROTNumber { get; set; } = string.Empty;
    public string AWBNumber { get; set; } = string.Empty;
    public string HouseAWBNumber { get; set; } = string.Empty;
    public string? MovementType { get; set; }
    public string? TripType { get; set; }
    public string FlightNumber { get; set; } = string.Empty;
    public string? TerminalLocation { get; set; }
    public Models.Company? Terminal { get; set; }
    public string? TerminalLocationName {get; set; }
    public DateOnly? ETA { get; set; }
    public string? SealNumber { get; set; }
    public string? ForwardingRemarks { get; set; }
    public string? HaulierRemarks { get; set; }
    public string? TerminalRemarks { get; set; }
    public string? ForwardingId { get; set; }
    public Models.Company? Forwarding { get; set; }
    public string? ForwardingName {get; set; }
    public string? AirlineId { get; set; }
    public Models.Company? Airline { get; set; }
    public string? AirlineName { get; set; }
    public string? BillingParty { get; set; }
    public string? CustomFormType { get; set; }
    public string? CustomFormNo { get; set; }
    public string? CustomReceiptNo { get; set; }
    public string? DICNumber { get; set; }
    public string? ZBNumber { get; set; }
    public int? TruckQuantity { get; set; }
    public string? CarrierReferenceNumber { get; set; }
    public int? TotalPackageQuantity { get; set; }
    public int? UpdatedTotalPackageQuantity { get; set; }
    public double? Weight { get; set; }
    public double? UpdatedWeight { get; set; }
    public string? ConsigneeId { get; set; }
    public Models.Company? ConsigneeCompany { get; set; }
    public string? SSMNumber { get; set; }
    public string? ExternalConsigneeName { get; set; }
    public string? ExternalConsigneeAddress { get; set; }
    public string? ExternalConsigneeContact { get; set; }
    public string? Size { get; set; }
    public string? BookingAgentId { get; set; }
    public Models.Company? BookingAgentCompany { get; set; }
}

public class AleBookingCreateDto
{
    [Required]
    public string ROTNumber { get; set; } = string.Empty;
    
    [Required]
    public string AWBNumber { get; set; } = string.Empty;
    
    [Required]
    public string HouseAWBNumber { get; set; } = string.Empty;

    public string? MovementType { get; set; }

    public string? TripType { get; set; }
    
    [Required]
    public string FlightNumber { get; set; } = string.Empty;

    public string? TerminalLocation { get; set; }
    
    public DateOnly? ETA { get; set; }
    public string? SealNumber { get; set; }
    public string? ForwardingRemarks { get; set; }
    public string? HaulierRemarks { get; set; }
    public string? TerminalRemarks { get; set; }

    public string? ForwardingId { get; set; }

    public string? AirlineId { get; set; }

    public string? BillingParty { get; set; }
    
    public string? CustomFormType { get; set; }
    public string? CustomFormNo { get; set; }
    public string? CustomReceiptNo { get; set; }
    public string? DICNumber { get; set; }
    public string? ZBNumber { get; set; }
    public int? TruckQuantity { get; set; }
    public string? CarrierReferenceNumber { get; set; }
    public int? TotalPackageQuantity { get; set; }
    public double? Weight { get; set; }
    public string? ConsigneeId { get; set; }
    public string? SSMNumber { get; set; }
    public string? ExternalConsigneeName { get; set; }
    public string? ExternalConsigneeAddress { get; set; }
    public string? ExternalConsigneeContact { get; set; }
    public string? Size { get; set; }
    public string? BookingAgentId { get; set; }
    public int? UpdatedTotalPackageQuantity { get; set; }
    public double? UpdatedWeight { get; set; }
}

public class AleBookingUpdateDto : AleBookingCreateDto
{
    
}