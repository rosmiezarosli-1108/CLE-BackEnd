using System.ComponentModel.DataAnnotations;
using CLE_BackEnd.DTOs.Booking;
using CLE_BackEnd.Models;

namespace CLE_BackEnd.DTOs.Container;

public class ContainerDto
{
    public int ContainerId { get; set; }
    public string? ContainerNumber { get; set; }
    public string ContainerType { get; set; } = string.Empty;
    public string ContainerSize { get; set; } = string.Empty;
    public string? VGM { get; set; }
    public string? TrailerType { get; set; } = string.Empty;
    public string ConsigneeId { get; set; } = string.Empty;
    public Models.Company? Consignee { get; set; }
    public string ConsigneeName { get; set; } = string.Empty;
    public string HaulierId { get; set; } = string.Empty;
    public Models.Company? Haulier { get; set; }
    public string HaulierName { get; set; } = string.Empty;
    public string? DepotId { get; set; } = string.Empty;
    public Models.Company? Depot { get; set; }
    public string? DepotName { get; set; } = string.Empty;
    public string? PortId { get; set; } = string.Empty;
    public Models.Company? Port { get; set; }
    public string? PortName { get; set; } = string.Empty;
    public  DateOnly ROTDate { get; set; }
    public List<ContainerAddressDto> ToAddress { get; set; } = new List<ContainerAddressDto>();
    public string ROTNumber { get; set; } = string.Empty;
    public BookingDto? Booking { get; set; }
    public string Status { get; set; } = string.Empty;
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
    public DateTime? RTAcceptedTime { get; set; }
    public DateTime? RTEnrouteTime { get; set; }
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
    public List<ContainerAuditsDto> UpdateHistory { get; set; } = new List<ContainerAuditsDto>();
    public string? ReceivedBy { get; set; }
}

public class ContainerCreateDto
{
    public string? ContainerNumber { get; set; }
    
    [Required]
    public string ContainerType { get; set; } = string.Empty;
    
    [Required]
    public string ContainerSize { get; set; } = string.Empty;

    public string? VGM { get; set; }
    
    public string? TrailerType { get; set; } = string.Empty;
    
    [Required]
    public string ConsigneeId { get; set; } = string.Empty;
    
    public string? HaulierId { get; set; }
    
    public string? DepotId { get; set; }
    
    public string? PortId { get; set; }
    
    [Required]
    public string ROTNumber { get; set; } = string.Empty;

    [Required]
    public List<ContainerAddressDto> ToAddress { get; set; } = new List<ContainerAddressDto>();
    
    [Required(ErrorMessage = "ROT Date is required")]
    public DateOnly ROTDate { get; set; }
    
    [Required]
    public string Status { get; set; } = string.Empty;
    
    [Required]
    public DateTime AssignedTime { get; set; }
    public DateTime? EnrouteTime { get; set; }
    
    // public string? TimeStatus { get; set; }
    //
    // public int? TurnAroundTime { get; set; }
    //
    // public double? DGCRate { get; set; }
    //
    // public bool DGCReductionEligibility { get; set; }
    //
    // public double? DGCReduction { get; set; }
}

public class ContainerUpdateDto
{
    public string? ContainerNumber { get; set; }
    public string ContainerType { get; set; } = string.Empty;
    public string ContainerSize { get; set; } = string.Empty;
    public string? VGM { get; set; }
    public string? TrailerType { get; set; } = string.Empty;
    public string ConsigneeId { get; set; } = string.Empty;
    public string? HaulierId { get; set; }
    public string? DepotId { get; set; }
    public string? PortId { get; set; }
    public string ROTNumber { get; set; } = string.Empty;
    public List<ContainerAddressDto> ToAddress { get; set; } = new List<ContainerAddressDto>();
    public DateOnly ROTDate { get; set; }
    public string? Status { get; set; } = string.Empty;
    public DateTime AssignedTime { get; set; }
    public DateTime? EnrouteTime { get; set; }
    public DateTime? AcceptedTime { get; set; }
    public DateTime? GatedInTime { get; set; }
    public DateTime? GatedOutTime { get; set; }
    public DateTime? DeliveredTime { get; set; }
    public DateTime? RFCTime { get; set; }
    public DateTime? RejectedTime { get; set; }
    public DateTime? DeletedTime { get; set; }
    public string? DeletedRemarks { get; set; }
    public DateTime? RTAssignedTime { get; set; }
    public DateTime? RTEnrouteTime { get; set; }
    public DateTime? RTAcceptedTime { get; set; }
    public DateTime? RTGatedInTime { get; set; }
    public DateTime? RTGatedOutTime { get; set; }
    public DateTime? RTDeliveredTime { get; set; }
    public DateTime? RTRFCTime { get; set; }
    public string? RejectedRemarks { get; set; }
    public List<ContainerAuditsDto> UpdateHistory { get; set; } = new();
    public string? ReceivedBy { get; set; }
    public string? UpdatedBy { get; set; } =  string.Empty;
}

public class ContainerAddressDto
{
    public string Address { get; set; } = string.Empty;
}

public class ContainerAuditsDto
{
    public string UpdatedBy { get; set; } = string.Empty;
    public DateTime UpdatedTime { get; set; }
    public string? Action { get; set; }
}
