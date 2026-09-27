using App.Config;
using App.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<CsvSettings>(
                configuration.GetSection(CsvSettings.SectionName));

            services.AddSingleton<CsvService>();

            return services;
        }
    }
}
