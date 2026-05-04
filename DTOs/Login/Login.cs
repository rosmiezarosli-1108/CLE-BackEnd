using CLE_BackEnd.Models;

namespace CLE_BackEnd.DTOs;

public class Login
{
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string CompanyRegion { get; set; } = string.Empty;
    public string Access { get; set; } = string.Empty;
}