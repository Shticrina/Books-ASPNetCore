using Microsoft.JSInterop;

namespace Client.Services.Auth;

public class TokenStorageService
{
    private readonly IJSRuntime _js;

    public TokenStorageService(IJSRuntime js)
    {
        _js = js;
    }

    public ValueTask SetTokenAsync(string token)
        => _js.InvokeVoidAsync("localStorage.setItem", "authToken", token);

    public ValueTask<string?> GetTokenAsync()
        => _js.InvokeAsync<string?>("localStorage.getItem", "authToken");

    public ValueTask RemoveTokenAsync()
        => _js.InvokeVoidAsync("localStorage.removeItem", "authToken");
}