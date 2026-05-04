using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLE_BackEnd.Models;

public class BookingDocument
{
    [Key]
    public Guid BookingDocumentId { get; set; }
    
    [Required]
    public string DocumentType { get; set; } = string.Empty;
    
    [Required]
    public string FileName { get; set; } = string.Empty;
    
    [Required]
    public string FilePath { get; set; } = string.Empty;

    [Required] 
    public DateTime UploadDate { get; set; }
    
    [Required]
    public string ROTNumber { get; set; } = string.Empty;

    [ForeignKey("ROTNumber")] 
    public virtual Booking Booking { get; set; } = null!;
}