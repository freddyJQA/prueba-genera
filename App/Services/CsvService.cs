using App.Config;
using App.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace App.Services
{
    public class CsvService
    {
        private readonly CsvSettings _settings;

        public CsvService(IOptions<CsvSettings> options)
        {
            _settings = options.Value;
        }

        public List<Marcacion> Load()
        {
            var csvPath = GetCsvPath();

            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true,
            };

            try
            {
                using var reader = new StreamReader(csvPath);
                using var csv = new CsvReader(reader, csvConfig);

                csv.Context.RegisterClassMap<MarcacionCsvMap>();

                return [.. csv.GetRecords<Marcacion>()];
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en mapeo de datos CSV: {ex.Message}");
            }
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
