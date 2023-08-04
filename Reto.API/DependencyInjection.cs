using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reto.API.Converters;
using Reto.API.Filters;
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

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(new ValidationActionFilter());
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddMappings();

            return services;
        }
    }
}
