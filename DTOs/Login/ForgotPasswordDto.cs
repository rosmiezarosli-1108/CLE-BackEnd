namespace CLE_BackEnd.DTOs;

public class ForgotPasswordDto
{
    public string UserId { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}