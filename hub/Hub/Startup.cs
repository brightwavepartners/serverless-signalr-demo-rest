using Hub.Models.Configuration;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;

[assembly: FunctionsStartup(typeof(Hub.Startup))]

namespace Hub
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration= new ConfigurationBuilder()
                .SetBasePath(builder.GetContext().ApplicationRootPath)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("secret.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddAppConfiguration(configuration);

            var serviceProvider = builder.Services.BuildServiceProvider();
            var appInsightsOptions = serviceProvider.GetService<IOptions<AppInsights>>();
            var logAnalyticsWorkspaceOptions = serviceProvider.GetService<IOptions<LogAnalyticsWorkspace>>();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.ApplicationInsights(
                    new TelemetryConfiguration
                    {
                        InstrumentationKey = appInsightsOptions.Value.InstrumentationKey,
                    },
                    TelemetryConverter.Traces)
                .WriteTo.AzureAnalytics(
                    authenticationId: logAnalyticsWorkspaceOptions.Value.AuthenticationId,
                    workspaceId: logAnalyticsWorkspaceOptions.Value.WorkspaceId)
                .CreateLogger();

            Log.Information("Startup");
        }
    }
}
