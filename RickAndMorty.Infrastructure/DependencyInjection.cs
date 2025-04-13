using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Core.Interfaces;
using RickAndMorty.Infrastructure.Data;
using RickAndMorty.Infrastructure.Repositories;
using RickAndMorty.Infrastructure.Services;

namespace RickAndMorty.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AzureConnectionString");

            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__AzureConnectionString");
            }

            services.AddDbContext<RickAndMortyDbContext>((provider, options) =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddHttpClient<IApiDataReadService, ApiDataReadService>(option =>
            {
                option.BaseAddress = new Uri("https://rickandmortyapi.com/api/");
            }).AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(3)))
              .AddTransientHttpErrorPolicy(policy => policy.CircuitBreakerAsync(5, TimeSpan.FromSeconds(5)));

            services.AddMemoryCache();
            services.AddSingleton<ICacheService, MemoryCacheService>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<ICleanDatabaseService, CleanDatabaseService>();
         

            return services;
        }
    }
}