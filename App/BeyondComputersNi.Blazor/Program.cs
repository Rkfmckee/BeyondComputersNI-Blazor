using BeyondComputersNi.Blazor;
using BeyondComputersNi.Blazor.Authentication;
using BeyondComputersNi.Blazor.Interfaces;
using BeyondComputersNi.Blazor.Interfaces.Authentication;
using BeyondComputersNi.Blazor.Services;
using BeyondComputersNi.Blazor.Services.Authentication;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
    config.SnackbarConfiguration.PreventDuplicates = true;
    config.SnackbarConfiguration.NewestOnTop = true;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
    config.SnackbarConfiguration.VisibleStateDuration = 5000;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
});

builder.Services.AddHttpClient(builder.Configuration["Api:HttpClient"] ?? "")
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["Api:Url"] ?? throw new InvalidDataException("Base URL not set.")))
    .AddHttpMessageHandler<AuthenticationHandler>();

builder.Services.AddBlazoredLocalStorageAsSingleton();

builder.Services.AddScoped<AuthenticationStateProvider, BcniAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();

builder.Services.AddTransient<AuthenticationHandler>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IRefreshService, RefreshService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IComputerService, ComputerService>();

await builder.Build().RunAsync();
