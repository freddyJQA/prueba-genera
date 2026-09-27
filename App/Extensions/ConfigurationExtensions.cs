using Microsoft.Extensions.Configuration;

namespace App.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddAppConfiguration(
            this IConfigurationBuilder configuration)
        {
            return configuration
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(
                    "appsettings.json",
                    optional: false,
                    reloadOnChange: true);
        }
    }
}
