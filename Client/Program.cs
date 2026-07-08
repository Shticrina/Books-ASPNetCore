using Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client.Interfaces.Api;
using Client.Services.Api;
using Client.Services.UI;

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

await builder.Build().RunAsync();
