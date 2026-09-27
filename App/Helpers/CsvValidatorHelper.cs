using App.Models;
using System.Globalization;

namespace App.Helpers
{
    public static class CsvValidatorHelper
    {
        public static ClockingCsvValidatorResult Validate(ClockingCsv clockingCsv)
        {
            var result = new ClockingCsvValidatorResult(clockingCsv);

            ValidateRut(clockingCsv, result);
            ValidateDateTime(clockingCsv, result);
            ValidateType(clockingCsv, result);
            ValidateOrigen(clockingCsv, result);

            return result;
        }

        private static void ValidateRut(
            ClockingCsv clockingCsv,
            ClockingCsvValidatorResult result)
        {
            if (string.IsNullOrWhiteSpace(clockingCsv.Rut))
            {
                result.Errors.Add("El RUT es obligatorio.");
            }
            else if (!RutValidatorHelper.IsValid(clockingCsv.Rut))
            {
                result.Errors.Add($"RUT con dígito verificador inválido: '{clockingCsv.Rut}'.");
            }
        }

        private static void ValidateDateTime(
            ClockingCsv clockingCsv,
            ClockingCsvValidatorResult result)
        {
            if (!DateTime.TryParseExact(
                clockingCsv.FechaHora,
                "yyyy-MM-dd HH:mm:ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _))
            {
                result.Errors.Add($"Fecha_hora inválida: '{clockingCsv.FechaHora}'.");
            }
        }

        private static void ValidateType(
            ClockingCsv clockingCsv,
            ClockingCsvValidatorResult result)
        {
            if (clockingCsv.Type is not ("E" or "S"))
            {
                result.Errors.Add($"Tipo inválido: '{clockingCsv.Type}'.");
            }
        }

        private static void ValidateOrigen(
            ClockingCsv clockingCsv,
            ClockingCsvValidatorResult result)
        {
            if (clockingCsv.Origen is not ("RELOJ" or "APP"))
            {
                result.Errors.Add($"Origen inválido: '{clockingCsv.Origen}'.");
            }
        }
    }
}
