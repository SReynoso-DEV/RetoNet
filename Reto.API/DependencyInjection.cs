using Microsoft.EntityFrameworkCore;
using Reto.API.Mapping;
using Reto.API.Middlewares;
using Reto.Domain.Interfaces;
using Reto.Infraestructure.Data;
using Reto.Infraestructure.Repositories;

namespace Reto.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddScoped<ExceptionMiddleware>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddMappings();

            return services;
        }
    }
}
