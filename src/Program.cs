using MaskedEmails.Extensions.Logging.Syslog;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
	.ConfigureFunctionsWorkerDefaults()
	.ConfigureServices((hostBuilderContext, services) => { 
        var configuration = services.GetConfiguration();
        services.AddLogging(builder =>
        {
            builder.AddSyslog(configuration!);
        });	})
	.Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("The masked-emails daemon started successfully.");

host.Run();

logger.LogInformation("The masked-emails daemon stopped.");
