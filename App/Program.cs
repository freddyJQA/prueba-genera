using App.Extensions;
using App.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddAppConfiguration();
builder.Services.AddAppServices(builder.Configuration);

var connectionString = builder.Configuration.GetConnectionString("Default");

var host = builder.Build();

var csvPath = host.Services
    .GetRequiredService<CsvService>()
    .GetCsvPath();

if (!File.Exists(csvPath))
{
    Console.WriteLine($"No se encontró el archivo CSV en: {csvPath}");
    return;
}

string[] lineas = File.ReadAllLines(csvPath);

Console.WriteLine($"Archivo cargado correctamente: {csvPath}");
Console.WriteLine($"Total de líneas leídas: {lineas.Length}");

foreach (var linea in lineas.Take(5))
{
    Console.WriteLine(linea);
}
