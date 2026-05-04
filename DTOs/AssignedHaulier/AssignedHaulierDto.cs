using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLE_BackEnd.DTOs.AssignedHaulier;

public class AssignedHaulierDto
{
    public int Id { get; set; }
    public string DriverName { get; set; } = string.Empty;
    public string PMNumber { get; set; } = string.Empty;
    public string TimeSlot { get; set; } = string.Empty;
    public int ContainerId { get; set; }
    public string ROTNumber { get; set; } = string.Empty;
    public string HaulierId { get; set; } = string.Empty;
    public string HaulierName { get; set; } = string.Empty;
}

public class AssignedHaulierCreateDto
{

    [Required] 
    public string DriverName { get; set; } = string.Empty;

    [Required] 
    public string PMNumber { get; set; } = string.Empty;

    [Required] 
    public string TimeSlot { get; set; } = string.Empty;

    [Required] 
    public int ContainerId { get; set; }
    
    [Required] 
    public string ROTNumber { get; set; } = string.Empty;

    [Required]
    public string HaulierId { get; set; } = string.Empty;
}

public class AssignedHaulierUpdateDto : AssignedHaulierCreateDto
{
    [Required]
    public int Id { get; set; }
}