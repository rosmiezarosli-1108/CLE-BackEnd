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
        
        // SAFARI COMPLIANCE FIX: Raw token string is passed via JSON payload, avoiding cookie storage completely
        return Ok(new
        {
            Token = token, // <-- Safely captured by frontend localStorage
            user.UserId,
            user.FullName,
            user.Access,
            Role = user.Company?.Role ?? "User", 
            user.CompanyName
        });
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        return Ok(new { message = "Logged out" });
    }
}