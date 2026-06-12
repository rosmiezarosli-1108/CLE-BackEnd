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
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        var authResult = await _authService.AuthenticateAsync(request.UserId, request.Password, request.CompanyRegion, request.Access);

        if (authResult.User == null)
        {
            return Unauthorized(new { message = authResult.ErrorMessage });
        }
    
        var user = authResult.User;
        var token = _tokenService.GenerateToken(user);
    
        // Return the token inside the JSON body instead of setting a Cookie
        return Ok(new
        {
            Token = token, // <-- Add this line
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

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.ResetPasswordAsync(request.UserId, request.EmailAddress, request.NewPassword);

        if (!result)
        {
            return BadRequest(new { message = "Invalid User ID or Email Address verification failed." });
        }
        
        return Ok(new { message = "Your password has been reset successfully!" });
    }
}