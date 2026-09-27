using App.Config;
using Microsoft.Extensions.Options;

namespace App.Services
{
    public class CsvService
    {
        private readonly CsvSettings _settings;

        public CsvService(IOptions<CsvSettings> options)
        {
            _settings = options.Value;
        }

        public string GetCsvPath()
        {
            var csvPath = Path.Combine(
                AppContext.BaseDirectory,
                _settings.FilePath);

            if (!File.Exists(csvPath))
            {
                throw new FileNotFoundException($"No se encontró el archivo CSV en: {csvPath}");
            }

            return csvPath;
        }
    }
}
