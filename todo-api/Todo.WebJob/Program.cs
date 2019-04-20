using Microsoft.Azure.WebJobs;
using Todo.Core.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace Todo.WebJob
{
    class Program
    {
        static void Main()
        {
            var config = new JobHostConfiguration("UseDevelopmentStorage=true");

            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }

            DoThings(config);

            var host = new JobHost(config);
            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }

        private static void DoThings(JobHostConfiguration config)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            serviceCollection.AddSingleton<Functions>();
            serviceCollection.AddSingleton(new DiscordClient("https://discordapp.com/api/webhooks/568553400803786778/27l9zIA1D8BbntAhcNfkwzMJ47euGm95OyrA_j9qtItFPg3bC156lDOHOFwwtTJFfgzk"));

            serviceCollection.AddLogging(builder =>
            {
                builder.AddConsole().AddApplicationInsights("41562bda-e522-49e1-a729-9c867ecf3547");
            });

            config.JobActivator = new ServiceCollectionJobActivator(serviceCollection);
        }
    }
}
