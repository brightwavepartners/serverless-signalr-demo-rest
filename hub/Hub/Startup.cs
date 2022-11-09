using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Serilog;

[assembly: FunctionsStartup(typeof(Hub.Startup))]

namespace Hub
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("secret.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.ApplicationInsights(
                    new TelemetryConfiguration
                    {
                        InstrumentationKey = ""
                    },
                    TelemetryConverter.Traces)
                .WriteTo.AzureAnalytics(
                    workspaceId: "",
                    authenticationId: "")
                .CreateLogger();

            Log.Information("Startup");
        }
    }
}
