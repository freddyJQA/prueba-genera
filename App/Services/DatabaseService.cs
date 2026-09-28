using App.Helpers;
using App.Models;
using App.Repositories;

namespace App.Services
{
    public class DatabaseService
    {
        private readonly DatabaseRepository _repository;

        public DatabaseService(DatabaseRepository repository)
        {
            _repository = repository;
        }

        public async Task Save(List<ClockingCsv> clockingsCsv)
        {
            var clockingsToInsert = new List<Clocking>();

            var validatedCsvRows = clockingsCsv.Select(CsvValidatorHelper.Validate).ToList();
            var validCsvRows = validatedCsvRows.Where(x => x.IsValid);
            var invalidCsvRows = validatedCsvRows.Where(x => !x.IsValid).ToList();

            var rutsCsv = validCsvRows.Select(x => RutNormalizedHelper.Normalize(x.ClockingCsv.Rut!)).ToList();
            var workersIds = await _repository.GetWorkersIdsByRuts(rutsCsv);

            foreach (var csvRow in validCsvRows)
            {
                var rutNormalized = RutNormalizedHelper.Normalize(csvRow.ClockingCsv.Rut!);

                if (workersIds.ContainsKey(rutNormalized))
                {
                    var clocking = ClockingCsv.MapToClocking(csvRow.ClockingCsv, workersIds[rutNormalized]);

                    clockingsToInsert.Add(clocking);
                }
                else
                {
                    csvRow.Errors.Add($"RUT no asociado a ningún trabajador: '{csvRow.ClockingCsv.Rut!}'.");

                    invalidCsvRows.Add(csvRow);
                }
            }

            await _repository.InsertMany(clockingsToInsert);
        }
    }
}
