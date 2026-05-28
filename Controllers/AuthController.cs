using CLE_BackEnd.DTOs;
using CLE_BackEnd.Models;
using CLE_BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CLE_BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        
        // SAFARI/iOS COMPATIBLE COOKIE CONFIGURATION
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,       
            Secure = true,                // Must be true for Cross-Site HTTPS tracking (Render)
            SameSite = SameSiteMode.None,  // Must be None to prevent Vercel from losing the cookie
            Path = "/",
            Domain = null,                // Keeps cookie tied precisely to the issuing server instance
            MaxAge = TimeSpan.FromDays(7)  // Using MaxAge instead of Expires to bypass Safari UTC parsing bugs
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
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/"
        };
        
        Response.Cookies.Delete("userToken", cookieOptions);
        return Ok(new { message = "Logged out" });
    }
}