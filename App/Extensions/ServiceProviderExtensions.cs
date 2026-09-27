using App.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace App.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static CsvSettings GetCsvSettings(this IServiceProvider services)
        {
            return services
                .GetRequiredService<IOptions<CsvSettings>>()
                .Value;
        }
    }
}
