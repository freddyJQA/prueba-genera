namespace App.Helpers
{
    public class RutNormalizedHelper
    {
        public static string Normalize(string rut)
        {
            var normalized = rut
                .Replace(".", string.Empty)
                .Trim()
                .TrimStart('0')
                .ToUpperInvariant();

            if (normalized.Length < 2)
                return normalized;

            return $"{normalized[..^1]}-{normalized[^1]}".ToUpperInvariant();
        }
    }
}
