using MaskedEmails.Extensions.Logging.Syslog;

[assembly: FunctionsStartup(typeof(Startup))]

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        ConfigureServices(builder.Services);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        var configuration = services.GetConfiguration();
        services.AddLogging(builder =>
        {
            builder.AddSyslog(configuration!);
        });
    }
}

public static class IServiceCollectionConfigurationExtensions
{
    public static IConfiguration? GetConfiguration(this IServiceCollection services)
        => services.BuildServiceProvider().GetService<IConfiguration>();
}