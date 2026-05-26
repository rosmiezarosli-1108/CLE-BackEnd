using CLE_BackEnd.DTOs;
using CLE_BackEnd.Models;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CLE_BackEnd.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;

    public AuthController(IAuthService authService,  ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login request)
    {
        var authResult = await _authService.AuthenticateAsync(request.UserId, request.Password, request.CompanyRegion, request.Access);

        if (authResult.User == null)
        {
            return Unauthorized(new { message = authResult.ErrorMessage });
        }
        
        var user = authResult.User;
        
        var token = _tokenService.GenerateToken(user);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,       
            Secure = true,               // Kept true for Production SSL tracking (Render)
            SameSite = SameSiteMode.None, // Kept None to avoid Vercel Cookie rejection
            Path = "/",
            Domain = null,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        
        Response.Cookies.Append("userToken", token, cookieOptions);
        
        // FIXED: Using safe navigation operators (?.) to prevent 500 NullReference exceptions during serialization
        return Ok(new
        {
            user.UserId,
            user.FullName,
            user.Access,
            Role = user.Company?.Role ?? "User", // Safeguards if Company tracking link isn't attached natively
            user.CompanyName
        });
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        Response.Cookies.Delete("userToken");
        return Ok(new { message = "Logged out" });
    }
}