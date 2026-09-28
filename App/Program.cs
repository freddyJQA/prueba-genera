using App.Extensions;
using App.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddAppConfiguration();
builder.Services.AddAppServices(builder.Configuration);

var host = builder.Build();

var csvRows = host.Services.GetRequiredService<CsvService>().Load();

var resume = await host.Services.GetRequiredService<DatabaseService>().Save(csvRows);

Console.WriteLine($"Leídas: {resume.Total}");
Console.WriteLine($"Aceptadas: {resume.Accepted}");
Console.WriteLine($"Rechazadas: {resume.Rejected}");