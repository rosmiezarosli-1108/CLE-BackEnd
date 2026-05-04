using System.ComponentModel.DataAnnotations;
using CLE_BackEnd.Models;
namespace CLE_BackEnd.DTOs.User;

public class UserDto
{
    public string UserId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyCode { get; set; } = string.Empty;
    public Models.Company? Company { get; set; }
    public string Access { get; set; } = string.Empty;
    public string AccessLevel { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string? ContactNumber { get; set; }
    public string? Status { get; set; }
    public string? UpdatedBy { get; set; }
    public string? CompanyAddress { get; set; }
}

public class UserCreateDto
{
    [Required]
    [StringLength(20)]
    public string UserId { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string CompanyCode { get; set; } = string.Empty;

    [Required]
    public string Access { get; set; } = string.Empty;

    [Required]
    public string AccessLevel { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;

    public string? ContactNumber { get; set; }
    
    public string? Status { get; set; }
}

public class UserUpdateDto
{
    [Required]
    public string UserId { get; set; } = string.Empty; // Primary Key to find the record

    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string CompanyCode { get; set; } = string.Empty;

    [Required]
    public string Access { get; set; } = string.Empty;

    [Required]
    public string AccessLevel { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;

    public string? ContactNumber { get; set; }
    
    public string? Status { get; set; }
    
    public string? CurrentPassword { get; set; } 
    
    public string? NewPassword { get; set; }
}