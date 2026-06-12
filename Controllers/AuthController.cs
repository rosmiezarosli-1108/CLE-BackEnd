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

    public AuthController(IAuthService authService, ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        // 1. Authenticate the user details against the database via AuthService
        var authResult = await _authService.AuthenticateAsync(request.UserId, request.Password, request.CompanyRegion, request.Access);

        if (authResult.User == null)
        {
            return Unauthorized(new { message = authResult.ErrorMessage });
        }
    
        var user = authResult.User;
        
        // 2. Generate the secure JWT Token
        var token = _tokenService.GenerateToken(user);
    
        // 3. Return the token cleanly inside the JSON response body payload.
        // This allows your frontend (Axios) to store it securely in localStorage 
        // and bypasses cross-domain cookie blockages entirely.
        return Ok(new
        {
            Token = token, 
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
        // Since we are now relying entirely on local storage tokens on the client side,
        // this simply acknowledges a successful server request.
        return Ok(new { message = "Logged out successfully" });
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