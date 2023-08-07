using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reto.Domain.Interfaces;
using Reto.Infraestructure.Data;
using Reto.Infraestructure.Repositories;

namespace Reto.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("RetoDB");

            string DB_HOST = Environment.GetEnvironmentVariable("DB_HOST");
            string DB_NAME = Environment.GetEnvironmentVariable("DB_NAME");
            string DB_PASSWORD = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

            connectionString = connectionString
                .Replace("{DB_HOST}", DB_HOST)
                .Replace("{DB_NAME}", DB_NAME)
                .Replace("{DB_SA_PASSWORD}", DB_PASSWORD);
                
            services.AddDbContext<RetoContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });


            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
