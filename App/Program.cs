using App.Extensions;
using App.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddAppConfiguration();
builder.Services.AddAppServices(builder.Configuration);

var host = builder.Build();

var csvRows = host.Services.GetRequiredService<CsvService>().Load();

await host.Services.GetRequiredService<DatabaseService>().Save(csvRows);