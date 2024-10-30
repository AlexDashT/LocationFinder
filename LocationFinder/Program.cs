using LocationFinder.Client.Pages;
using LocationFinder.Components;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using LocationFinder.Client.Services;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services
       .AddAuth0WebAppAuthentication(options => {
            options.Domain = builder.Configuration["Auth0:Domain"];
            options.ClientId = builder.Configuration["Auth0:ClientId"];
        });
builder.Services.AddHttpClient("LocationFinder.ServerAPI", client => client.BaseAddress = new Uri("https://localhost:7098"));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("LocationFinder.ServerAPI"));
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<LocationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapGet("/Account/Login", async (HttpContext httpContext, string returnUrl = "/") =>
{
  var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
          .WithRedirectUri(returnUrl)
          .Build();

  await httpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
});

app.MapGet("/Account/Logout", async (HttpContext httpContext) =>
{
  var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
          .WithRedirectUri("/")
          .Build();

  await httpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
  await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
}) ;

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

app.Run();
