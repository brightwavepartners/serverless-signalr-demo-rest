using Hub.Models.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hub
{
    internal static class ConfigurationServiceCollectionExtensions
    {
        #region Methods

        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AppInsights>(config.GetSection(nameof(AppInsights)));
            services.Configure<LogAnalyticsWorkspace>(config.GetSection(nameof(LogAnalyticsWorkspace)));

            return services;
        }

        #endregion
    }
}
