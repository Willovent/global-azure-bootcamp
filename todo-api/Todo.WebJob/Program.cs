using Todo.Core.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Todo.WebJob
{
    class Program
    {
        static async Task Main()
        {
            await new HostBuilder()
               .ConfigureHostConfiguration(builder =>
               {
                   builder.SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", false, true);
#if DEBUG
                   builder.AddUserSecrets<Program>();
#endif
               })
               .ConfigureLogging((context, builder) =>
               {
                   builder.AddConfiguration(context.Configuration.GetSection("Logging"));
#if DEBUG
                   builder.AddConsole();
#endif
               })
               .ConfigureServices((context, services) =>
               {
                   services.AddSingleton<Functions>();
                   services.AddScoped<TodoTelemetryService>(_ => new TodoTelemetryService(context.Configuration["ApplicationInsights:InstrumentationKey"]));
                   services.AddSingleton(new DiscordClient(context.Configuration["DiscordWebhook"]));
               })
               .ConfigureWebJobs(builder => builder.AddAzureStorage())
               .RunConsoleAsync();
        }
    }
}
