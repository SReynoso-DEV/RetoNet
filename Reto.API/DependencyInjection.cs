using Microsoft.AspNetCore.Mvc;
using Reto.API.Converters;
using Reto.API.Filters;
using Reto.API.Mapping;
using Reto.API.Middlewares;

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
                options.ModelBinderProviders.Insert(0, new Converters.DateTimeModelBinderProvider());
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
