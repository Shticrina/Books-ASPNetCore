using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Client.Services.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Auth;

public class JwtAuthenticationStateProvider
    : AuthenticationStateProvider
{
    private readonly TokenStorageService tokenStorage;

    public JwtAuthenticationStateProvider(
        TokenStorageService tokenStorage)
    {
        this.tokenStorage = tokenStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var token = await tokenStorage.GetTokenAsync();

            if (string.IsNullOrWhiteSpace(token))
                return Anonymous();

            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token))
                return Anonymous();

            var jwt = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(jwt.Claims, "jwt");

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
        catch
        {
            return Anonymous();
        }
    }

    public void NotifyUserAuthentication()
    {
        NotifyAuthenticationStateChanged(
            GetAuthenticationStateAsync());
    }

    public void NotifyUserLogout()
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(Anonymous()));
    }

    private AuthenticationState Anonymous()
    {
        return new AuthenticationState(
            new ClaimsPrincipal(
                new ClaimsIdentity()));
    }
}