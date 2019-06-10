using Docker.Jobs;
using Docker.Services;
using Docker.SimpleDAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Docker
{
    public static class BootstrapApplication
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.AddScoped<IDapperCommands, DapperCommands>();
            services.AddScoped<IExemploJob, ExemploJob>();
            services.AddScoped<IExemploService, ExemploService>();

            return services;
        }
    }
}
