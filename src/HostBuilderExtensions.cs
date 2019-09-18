using System;
using System.IO;
using MaskedEmails.Extensions.Logging.Syslog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace maskedd
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
        public static IHostBuilder ConfigureLogging(this IHostBuilder host, LogLevel level)
        {
            return host
                .ConfigureLogging((hostContext, config) =>
                {
                    config.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                    config.AddConsole();
                    config.AddSyslog(level);
                });
        }

        public static IHostBuilder ConfigureServices(this IHostBuilder host, LogLevel level)
        {
            return host
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging(builder =>
                    {
                        var s = hostContext.Configuration.GetSection("Logging");
                        var c = s.GetSection("Console");
                        var d = c.GetValue(typeof(String), "LogLevel");
                        builder.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                        builder.AddConsole();
                        builder.AddSyslog(level);
                    });
                });
        }
        public static IHostBuilder ConfigureAppConfiguration(this IHostBuilder host, string[] args)
        {
            return host
                .ConfigureAppConfiguration((hostContext, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());
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