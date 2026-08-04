using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Interfaces;
using Shared.DTOs.Auth;
using Shared.Responses;

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
            return Ok(new ApiResponse<AuthResponseDto>
                {
                    Success = true,
                    Message = "Registration successful.",
                    StatusCode = 200,
                    Data = response
                });
        }
        catch (AuthValidationException ex)
        {
            foreach (var error in ex.Errors)
            {
                foreach (var message in error.Value)
                {
                    ModelState.AddModelError(error.Key, message);
                }
            }

            return ValidationProblem(ModelState);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(
        LoginRequestDto request)
    {
        try
        {
            var response = await authService.LoginAsync(request);
            return Ok(new ApiResponse<AuthResponseDto>
            {
                Success = true,
                Message = "Login successful.",
                StatusCode = 200,
                Data = response
            });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new ApiResponse<AuthResponseDto>
            {
                Success = false,
                Message = "Invalid email or password.",
                StatusCode = 401
            });
        }
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<CurrentUserDto>> Me()
    {
        var id = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        var email = User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;

        return Ok(new CurrentUserDto
        {
            Id = id!,
            Email = email!
        });
    }
}