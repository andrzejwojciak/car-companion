using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using CarCompanion.UI.Services;
using CarCompanion.UI.Services.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CarCompanion.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            
            builder.Services
                  .AddBlazorise( options =>
                  {
                      options.ChangeTextOnKeyPress = true;
                  } )
                  .AddBootstrapProviders()
                  .AddFontAwesomeIcons();
            
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IRequestSenderService, RequestSenderService>();
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
            builder.Services.AddScoped<IExpenseService, ExpenseService>();
            builder.Services.AddScoped<IShareKeyService, ShareKeyService>();

            var host = builder.Build();

            host.Services
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();

            await host.RunAsync();
            
            
        }
    }
}
