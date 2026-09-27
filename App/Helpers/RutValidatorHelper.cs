using System.Text.RegularExpressions;

namespace App.Helpers
{
    public class RutValidatorHelper
    {
        public static bool IsValid(string rut)
        {
            if (string.IsNullOrWhiteSpace(rut))
                return false;

            string normalizedRut = RutNormalizedHelper.Normalize(rut).Replace("-", string.Empty);

            if (normalizedRut.Length < 2)
                return false;   

            string numberRut = normalizedRut[..^1];
            char expectedCheckDigit = normalizedRut[^1];

            if (!numberRut.All(char.IsDigit))
                return false;

            char checkedDigit = CheckDigit(numberRut);

            return expectedCheckDigit == checkedDigit;
        }

        private static char CheckDigit(string numberRut)
        {
            int suma = 0;
            int factor = 2;

            for (int i = numberRut.Length - 1; i >= 0; i--)
            {
                suma += (numberRut[i] - '0') * factor;
                factor = factor == 7 ? 2 : factor + 1;
            }

            int resto = 11 - (suma % 11);

            return resto switch
            {
                11 => '0',
                10 => 'K',
                _ => (char)('0' + resto)
            };
        }
    }
}
