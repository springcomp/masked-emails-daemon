public static class IServiceCollectionConfigurationExtensions
{
    public static IConfiguration? GetConfiguration(this IServiceCollection services)
        => services.BuildServiceProvider().GetService<IConfiguration>();
}