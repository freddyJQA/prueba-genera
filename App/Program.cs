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

var recorsd = host.Services
    .GetRequiredService<CsvService>()
    .Load();

var h = recorsd;