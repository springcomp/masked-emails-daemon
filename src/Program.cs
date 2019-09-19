using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MaskedEmails
{
    class Program
    {
        static void Main(string[] args)
            => MainAsync(args)
            .GetAwaiter()
            .GetResult()
            ;

        private static async Task MainAsync(string[] args)
        {
            var host = new HostBuilder()
                    .UseConsoleLifetime() // Handle CTRL+C
                    .ConfigureHostConfiguration(args)
                    .ConfigureAppConfiguration(args)
                    .ConfigureLogging()
                    .ConfigureServices()
                    .ConfigureWebJobs()
                    .Build()
                ;

            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            logger.LogInformation("The masked-emails daemon started successfully.");

            await host.RunAsync();

            logger.LogInformation("The masked-emails daemon stopped.");
        }
    }
}
