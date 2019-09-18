using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace maskedd
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
            var cmdLine = CommandLine.Parse(args);
            var level = cmdLine.Level;

            var host = new HostBuilder()
                    .UseConsoleLifetime() // Handle CTRL+C
                    .ConfigureHostConfiguration(args)
                    .ConfigureAppConfiguration(args)
                    .ConfigureLogging(level)
                    .ConfigureServices(level)
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
