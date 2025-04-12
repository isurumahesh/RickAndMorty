using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Application.Validators;

namespace RickAndMorty.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddValidatorsFromAssemblyContaining<CharacterValidator>();

            return services;
        }
    }
}