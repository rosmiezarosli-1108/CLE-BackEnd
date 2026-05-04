using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLE_BackEnd.Models;

public class User
{
    [Key]
    [Column(TypeName = "varchar(20)")]
    public string UserId { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
    
    [Required]
    public string FullName { get; set; } = string.Empty;
    
    [Required]
    public string CompanyName { get; set; } = string.Empty;
    
    [Required]
    public string CompanyCode { get; set; } = string.Empty;
    
    [ForeignKey("CompanyCode")]
    public virtual Company Company { get; set; } = null!;
    
    [Required]
    public string Access { get; set; } = string.Empty;
    
    [Required]
    public string AccessLevel { get; set; } = string.Empty;
    
    [Required]
    public string EmailAddress { get; set; } = string.Empty;
    
    public string? ContactNumber { get; set; }
    
    public string? Status { get; set; }
    
    public string? UpdatedBy { get; set; }

}