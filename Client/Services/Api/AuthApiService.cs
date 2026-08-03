using Shared.DTOs.Auth;
using Client.Interfaces.Api;

namespace Client.Services.Api;
public class AuthApiService : BaseApiService, IAuthApiService
{
    private readonly HttpClient _http;

    public AuthApiService(HttpClient http)
    {
        _http = http;
    }

    public Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        => PostAsync<LoginRequestDto, AuthResponseDto>(
            _http,
            "api/auth/login",
            request);

    public Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        => PostAsync<RegisterRequestDto, AuthResponseDto>(
            _http,
            "api/auth/register",
            request);

    public Task<CurrentUserDto?> GetCurrentUserAsync()
        => GetAsync<CurrentUserDto?>(_http, "api/auth/me");
}