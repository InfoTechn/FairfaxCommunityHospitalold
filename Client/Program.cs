using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var baseAddress = builder.Configuration["BaseAddress"] ?? builder.HostEnvironment.BaseAddress;
            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(baseAddress) });
            
            //use synchronous JavaScript interop in my entire application to avoid casting JSRuntime to IJSInProcessRuntime on every call
            builder.Services.AddSingleton<IJSInProcessRuntime>(services => (IJSInProcessRuntime)services.GetRequiredService<IJSRuntime>());

            await builder.Build().RunAsync();
        }
    }
}