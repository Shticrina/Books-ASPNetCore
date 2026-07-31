using Shared.DTOs.Auth;

namespace Server.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequest);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequest);
    // Task LogoutAsync();
    // Task<AuthResponseDto> GetCurrentUserAsync();
}