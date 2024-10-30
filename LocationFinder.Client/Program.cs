using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using LocationFinder.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddHttpClient("LocationFinder.ServerAPI", client => client.BaseAddress = new Uri("https://localhost:7098"));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("LocationFinder.ServerAPI"));
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<LocationService>();

await builder.Build().RunAsync();
