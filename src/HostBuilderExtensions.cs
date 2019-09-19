using System.IO;
using System.Reflection;
using MaskedEmails.Extensions.Logging.Syslog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Syslog.Framework.Logging;

namespace MaskedEmails
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureWebJobs(this IHostBuilder host)
        {
            return host
                .ConfigureWebJobs(builder =>
                {
                    builder.AddAzureStorageCoreServices();
                    builder.AddAzureStorage(options => { options.BatchSize = 1; });
                });
        }
        public static IHostBuilder ConfigureLogging(this IHostBuilder host)
        {
            return host
                .ConfigureLogging((hostContext, config) =>
                {
                    var logging = hostContext.Configuration.GetSection("Logging");

                    config.AddConfiguration(logging);
                    config.AddConsole();
                    config.AddSyslog(hostContext);
                });
        }

        public static IHostBuilder ConfigureServices(this IHostBuilder host)
        {
            return host
                .ConfigureServices((hostContext, services) =>
                    {
                    });
        }
        public static IHostBuilder ConfigureAppConfiguration(this IHostBuilder host, string[] args)
        {
            return host
                .ConfigureAppConfiguration((hostContext, builder) =>
                {
                    var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? Directory.GetCurrentDirectory();
                    builder.SetBasePath(directory);

                    builder.AddEnvironmentVariables(prefix: "ASPNETCORE_");
                    builder.AddJsonFile($"appsettings.json", true);
                    builder.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true);
                    builder.AddCommandLine(args);
                });
        }
        public static IHostBuilder ConfigureHostConfiguration(this IHostBuilder host, string[] args)
        {
            return host
                .ConfigureHostConfiguration(builder =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                    builder.AddEnvironmentVariables(prefix: "ASPNETCORE_");
                    builder.AddCommandLine(args);
                });
        }
    }
}