using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using Shared.DTOs.Auth;
// using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Server.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(
        RegisterRequestDto request)
    {
        try
        {
            var response = await authService.RegisterAsync(request);

            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(
        LoginRequestDto request)
    {
        try
        {
            var response = await authService.LoginAsync(request);

            return Ok(response);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });
        }
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<CurrentUserDto>> Me()
    {
        // var email = User.FindFirst(ClaimTypes.Email)?.Value;
        // var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var id = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        var email = User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;

        return Ok(new CurrentUserDto
        {
            Id = id!,
            Email = email!
        });
    }
}