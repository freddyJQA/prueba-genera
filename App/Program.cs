using Microsoft.Extensions.Configuration;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string? connectionString = configuration.GetConnectionString("Default");

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("No se encontró la cadena de conexión.");
    return;
}

Console.WriteLine($"Cadena de conexión cargada: {connectionString}");
