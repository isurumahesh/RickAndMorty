using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Core.Interfaces;
using RickAndMorty.Infrastructure.Data;
using RickAndMorty.Infrastructure.Repositories;

namespace RickAndMorty.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<RickAndMortyDbContext>((provider, options) =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddHttpClient<ICharacterService, CharacterService>(option =>
            {
                option.BaseAddress = new Uri("https://rickandmortyapi.com/api/");
            });

            services.AddScoped<ICharacterRepository, CharacterRepository>();

            return services;
        }
    }
}