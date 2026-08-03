using System.Net.Http.Headers;

namespace Client.Services.Auth;

public class AuthorizationMessageHandler : DelegatingHandler
{
    private readonly TokenStorageService tokenStorage;

    public AuthorizationMessageHandler(TokenStorageService tokenStorage)
    {
        this.tokenStorage = tokenStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await tokenStorage.GetTokenAsync();

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}