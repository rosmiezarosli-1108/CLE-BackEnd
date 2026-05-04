using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CLE_BackEnd.DTOs.BookingDocument;

public class BookingDocumentDto
{
    public Guid BookingDocumentId { get; set; }
    public string DocumentType { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public DateTime UploadDate { get; set; }
    public string ROTNumber { get; set; } = string.Empty;
    public Models.Booking? Booking { get; set; }
}

public class BookingDocumentCreateDto
{
    [Required]
    public string DocumentType { get; set; } = string.Empty;
    
    [Required]
    public string FileName { get; set; } = string.Empty;
    
    public string? FilePath { get; set; }

    [Required] 
    public DateTime UploadDate { get; set; }
    
    [Required]
    public string ROTNumber { get; set; } = string.Empty;
    
    public IFormFile? File { get; set; }
}

public class BookingDocumentUpdateDto
{
    [Key]
    public Guid BookingDocumentId { get; set; }
    
    [Required]
    [FromForm(Name = "documentType")]
    public string DocumentType { get; set; } = string.Empty;
    
    [FromForm(Name = "fileName")]
    public string? FileName { get; set; }
    
    public string? FilePath { get; set; } = string.Empty;
    
    public DateTime? UploadDate { get; set; }
    
    public string? ROTNumber { get; set; } = string.Empty;
    
    [FromForm(Name = "file")]
    public IFormFile? File { get; set; }
}