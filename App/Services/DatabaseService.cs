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
            var validatedRows = ValidateRows(clockingsCsv);

            var validRows = validatedRows
                .Where(x => x.IsValid)
                .ToList();

            var invalidRows = validatedRows
                .Where(x => !x.IsValid)
                .ToList();

            var workersIds = await GetWorkersIds(validRows);

            var clockings = MapToClockings(
                validRows,
                workersIds,
                invalidRows);

            await _repository.InsertMany(clockings);

            await CsvService.GenerateInvalidCsv(invalidRows);
        }

        private List<ClockingCsvValidatorResult> ValidateRows(List<ClockingCsv> clockingsCsv)
        {
            return [.. clockingsCsv.Select(CsvValidatorHelper.Validate)];
        }

        private async Task<Dictionary<string, int>> GetWorkersIds(
            List<ClockingCsvValidatorResult> validRows)
        {
            var ruts = validRows
                .Select(x => RutNormalizedHelper.Normalize(x.ClockingCsv.Rut!))
                .ToList();

            return await _repository.GetWorkersIdsByRuts(ruts);
        }

        private static List<Clocking> MapToClockings(
            List<ClockingCsvValidatorResult> validRows,
            Dictionary<string, int> workersIds,
            List<ClockingCsvValidatorResult> invalidRows)
        {
            var clockings = new List<Clocking>();

            foreach (var row in validRows)
            {
                var rutNormalized = RutNormalizedHelper.Normalize(row.ClockingCsv.Rut!);

                if (!workersIds.TryGetValue(rutNormalized, out var workerId))
                {
                    row.Errors.Add($"RUT no asociado a ningún trabajador: '{row.ClockingCsv.Rut!}'.");

                    invalidRows.Add(row);
                    continue;
                }

                var clocking = ClockingCsv.MapToClocking(
                    row.ClockingCsv,
                    workerId);

                clockings.Add(clocking);
            }

            return clockings;
        }
    }
}
