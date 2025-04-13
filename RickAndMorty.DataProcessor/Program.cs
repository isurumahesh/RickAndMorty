using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RickAndMorty.Application;
using RickAndMorty.DataProcessor;
using RickAndMorty.Infrastructure;
using Serilog;

var host = Host.CreateDefaultBuilder(args)
     .UseSerilog((context, services, configuration) => configuration
        .MinimumLevel.Information()
        .WriteTo.Console()
        .WriteTo.File("logs/console-log-.txt", rollingInterval: RollingInterval.Day)
    )
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<ApplicationService>();
        services.AddApplicationDI();
        services.AddInfrastructureDI(context.Configuration);
    })
    .Build();

var app = host.Services.GetRequiredService<ApplicationService>();
await app.ReadData();