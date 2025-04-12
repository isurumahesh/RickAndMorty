using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RickAndMorty.Application;
using RickAndMorty.DataProcessor;
using RickAndMorty.Infrastructure;
using RickAndMorty.Infrastructure.Services;
using Serilog;

var host = Host.CreateDefaultBuilder(args)
     .UseSerilog((context, services, configuration) => configuration
        .MinimumLevel.Warning()
        .WriteTo.Console()  // Log to console
        .WriteTo.File("logs/console-log-.txt", rollingInterval: RollingInterval.Day) // Log to file
    )
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<ICharacterService, CharacterService>();
        services.AddSingleton<ApplicationService>();
        services.AddHttpClient<ICharacterService, CharacterService>(option =>
        {
            option.BaseAddress = new Uri("https://rickandmortyapi.com/api/");
        });
        services.AddApplicationDI();
        services.AddInfrastructureDI(context.Configuration);
    })
    .Build();

var app = host.Services.GetRequiredService<ApplicationService>();
await app.ReadData();