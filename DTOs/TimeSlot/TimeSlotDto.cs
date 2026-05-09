using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLE_BackEnd.DTOs.TimeSlot;

public class TimeSlotDto
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public string Time { get; set; } = string.Empty;
    public int? PickUpTotalSlot { get; set; }
    public int? DropOffTotalSlot { get; set; }
    public string DepotId { get; set; } = string.Empty;
    public Models.Company? Depot { get; set; }
}

public class TimeSlotCreateDto
{
    [Required]
    public DateOnly Date { get; set; }
    
    [Required]
    public string Time { get; set; } = string.Empty;
    
    public int? PickUpTotalSlot { get; set; }
    
    public int? DropOffTotalSlot { get; set; }
    
    [Required]
    public string DepotId { get; set; } = string.Empty;
}

public class TimeSlotUpdateDto
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
    public string DepotId { get; set; } = string.Empty;
}