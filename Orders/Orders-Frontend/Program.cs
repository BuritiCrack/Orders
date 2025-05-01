using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Orders_Frontend.AuthenticationProviders;
using Orders_Frontend.Repositories;
using Orders_Frontend.Services;

namespace Orders_Frontend;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7149/") });
        builder.Services.AddScoped<IRepository, Repository>();
        builder.Services.AddSweetAlert2();
        builder.Services.AddAuthorizationCore();

        builder.Services.AddScoped<AuthenticationProviderJWT>();
        builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(sp =>
            sp.GetRequiredService<AuthenticationProviderJWT>());
        builder.Services.AddScoped<ILoginServices, AuthenticationProviderJWT>(sp =>
            sp.GetRequiredService<AuthenticationProviderJWT>());

        await builder.Build().RunAsync();
    }
}