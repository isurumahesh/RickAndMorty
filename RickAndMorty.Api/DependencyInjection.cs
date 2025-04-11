using RickAndMorty.Infrastructure;

namespace RickAndMorty.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureDI(configuration);
            return services;
        }
    }
}