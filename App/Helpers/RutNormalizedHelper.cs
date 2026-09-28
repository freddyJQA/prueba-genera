namespace App.Helpers
{
    public static class RutNormalizedHelper
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

            return normalized.ToUpperInvariant();
        }
    }
}
