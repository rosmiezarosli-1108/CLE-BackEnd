using System.ComponentModel.DataAnnotations;
using CLE_BackEnd.DTOs.AleBooking;
using CLE_BackEnd.DTOs.Booking;
using CLE_BackEnd.DTOs.Container;

namespace CLE_BackEnd.DTOs.AleContainer;

public class AleContainerDto
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
    public string? TerminalId { get; set; } = string.Empty;
    public Models.Company? Terminal { get; set; }
    public string? TerminalName { get; set; } = string.Empty;
    public  DateOnly ROTDate { get; set; }
    public List<AleContainerAddressDto> ToAddress { get; set; } = new List<AleContainerAddressDto>();
    public string ROTNumber { get; set; } = string.Empty;
    public AleBookingDto? AleBooking { get; set; }
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
    public List <AleContainerAuditsDto> UpdateHistory { get; set; } = new List<AleContainerAuditsDto>();
    public string? ReceivedBy { get; set; }
    
    public DateTime? ApprovedAKPSTime { get; set; }
    public DateTime? ApprovedCustomsTime { get; set; }
    public DateTime? ApprovedBothTime { get; set; }
    public DateTime? RejectedBothTime { get; set; }
    public DateTime? ExamineBothTime { get; set; }
    
    public DateTime? TerminalGatedInTime { get; set; }
    public DateTime? TerminalGatedOutTime { get; set; }
    
    
    public DateTime? CustomAcceptedTime { get; set; }
    public DateTime? CustomRejectedTime { get; set; }
    
    public DateTime? CustomExamineTime { get; set; }

    public DateTime? AKPSAcceptedTime { get; set; }
    public DateTime? AKPSRejectedTime { get; set; }
    public DateTime? AKPSExamineTime { get; set; }
}

public class AleContainerCreateDto
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
    
    public string? TerminalId { get; set; }
    
    [Required]
    public string ROTNumber { get; set; } = string.Empty;

    [Required]
    public List<AleContainerAddressDto> ToAddress { get; set; } = new List<AleContainerAddressDto>();
    
    [Required(ErrorMessage = "ROT Date is required")]
    public DateOnly ROTDate { get; set; }
    
    [Required]
    public string Status { get; set; } = string.Empty;
    
    [Required]
    public DateTime AssignedTime { get; set; }
    
    public DateTime? EnrouteTime { get; set; }
}

public class AleContainerUpdateDto
{
    public string? ContainerNumber { get; set; }
    public string ContainerType { get; set; } = string.Empty;
    public string ContainerSize { get; set; } = string.Empty;
    public string? VGM { get; set; }
    public string? TrailerType { get; set; } = string.Empty;
    public string ConsigneeId { get; set; } = string.Empty;
    public string? HaulierId { get; set; }
    public string? TerminalId { get; set; }
    public string ROTNumber { get; set; } = string.Empty;
    public List<AleContainerAddressDto> ToAddress { get; set; } = new List<AleContainerAddressDto>();
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
    public List<AleContainerAuditsDto> UpdateHistory { get; set; } = new();
    public string? ReceivedBy { get; set; }
    public string? UpdatedBy { get; set; } =  string.Empty;
    public DateTime? ApprovedAKPSTime { get; set; }
    public DateTime? ApprovedCustomsTime { get; set; }
    public DateTime? ApprovedBothTime { get; set; }
    public DateTime? RejectedBothTime { get; set; }
    public DateTime? ExamineBothTime { get; set; }
    public DateTime? TerminalGatedInTime { get; set; }
    public DateTime? TerminalGatedOutTime { get; set; }
    
    public DateTime? CustomAcceptedTime { get; set; }
    public DateTime? CustomRejectedTime { get; set; }
    public DateTime? CustomExamineTime { get; set; }
    

    public DateTime? AKPSAcceptedTime { get; set; }
    public DateTime? AKPSRejectedTime { get; set; }
    public DateTime? AKPSExamineTime { get; set; }
  
}

public class AleContainerAddressDto
{
    public string Address { get; set; } = string.Empty;
}

public class AleContainerAuditsDto
{
    public string UpdatedBy { get; set; } = string.Empty;
    public DateTime UpdatedTime { get; set; }
    public string? Action { get; set; }
}