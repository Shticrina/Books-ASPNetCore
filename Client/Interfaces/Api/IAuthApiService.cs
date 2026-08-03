using Shared.DTOs.Auth;

public interface IAuthApiService
{
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);

    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);

    Task<CurrentUserDto?> GetCurrentUserAsync();
}