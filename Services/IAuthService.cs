namespace CLE_BackEnd.Services;

public class AuthResult
{
    public CLE_BackEnd.Models.User? User { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}

public interface IAuthService
{
    Task<AuthResult> AuthenticateAsync(string userId, string password, string region, string access);
}   