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
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,       
            // Note: Once you deploy to a production server with SSL, change these back to true and Strict
            Secure = false,          // Sent only over HTTPS (use false for localhost development)
            SameSite = SameSiteMode.Lax, // Protects against CSRF, change to strict later
            Path = "/",
            Domain = null,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        
        Response.Cookies.Append("userToken", token, cookieOptions);
        
        return Ok(new
        {
            user.UserId,
            user.FullName,
            user.Access,
            user.Company.Role,
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