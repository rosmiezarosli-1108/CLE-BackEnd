using System.ComponentModel.DataAnnotations;

namespace CLE_BackEnd.DTOs.AleTimeSlot;

public class AleTimeSlotDto
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public string Time { get; set; } = string.Empty;
    public int? PickUpTotalSlot { get; set; }
    public int? DropOffTotalSlot { get; set; }
    public string TerminalId { get; set; } = string.Empty;
    public Models.Company? Terminal { get; set; }
    public string? ChangeRemarks { get; set; }
}

public class AleTimeSlotCreateDto
{
    [Required]
    public DateOnly Date { get; set; }
    
    [Required]
    public string Time { get; set; } = string.Empty;
    
    public int? PickUpTotalSlot { get; set; }
    
    public int? DropOffTotalSlot { get; set; }
    
    [Required]
    public string TerminalId { get; set; } = string.Empty;
}

public class AleTimeSlotUpdateDto
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public DateOnly Date { get; set; }
    
    [Required]
    public string Time { get; set; } = string.Empty;
    
    public int? PickUpTotalSlot { get; set; }
    
    public int? DropOffTotalSlot { get; set; }
    
    [Required]
    public string TerminalId { get; set; } = string.Empty;
    
    public string? ChangeRemarks { get; set; }
}