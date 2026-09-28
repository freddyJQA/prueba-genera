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

        public List<ClockingCsv> Load()
        {
            var csvPath = GetCsvPath();

            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
            };

            try
            {
                using var reader = new StreamReader(csvPath);
                using var csv = new CsvReader(reader, csvConfig);

                csv.Context.RegisterClassMap<ClockingCsvMap>();

                var lineNumber = 2;
                var records = new List<ClockingCsv>();

                foreach (var record in csv.GetRecords<ClockingCsv>())
                {
                    record.LineNumber = lineNumber++;
                    records.Add(record);
                }

                return records;
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

        public static async Task GenerateInvalidCsv(
            List<ClockingCsvValidatorResult> invalidRows)
        {
            if (invalidRows.Count == 0)
                return;

            Directory.CreateDirectory("output");

            var filePath = Path.Combine(
                "output",
                $"rechazos.csv");

            await using var writer = new StreamWriter(filePath);

            await writer.WriteLineAsync("Línea;Rut;Motivo");

            foreach (var row in invalidRows)
            {
                var lineNumber = row.ClockingCsv.LineNumber;
                var rut = row.ClockingCsv.Rut;
                var error = string.Join(" | ", row.Errors);

                await writer.WriteLineAsync($"{lineNumber},{rut},{error}");
            }
        }
    }
}
