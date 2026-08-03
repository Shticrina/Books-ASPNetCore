using Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client.Interfaces.Api;
using Client.Services.Api;
using Client.Services.UI;
using Client.Services.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Client.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7188/") }); // https profile
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5191/") }); // http profile

builder.Services.AddScoped<IBookApiService, BookApiService>();
builder.Services.AddScoped<ICategoryApiService, CategoryApiService>();
builder.Services.AddScoped<IAuthorApiService, AuthorApiService>();
builder.Services.AddScoped<ToastService>();
builder.Services.AddScoped<DialogService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<TokenStorageService>();
builder.Services.AddScoped<IAuthApiService, AuthApiService>();
builder.Services.AddScoped<JwtAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(
    sp => sp.GetRequiredService<JwtAuthenticationStateProvider>());

await builder.Build().RunAsync();
