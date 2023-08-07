using Reto.API;
using Reto.API.Middlewares;
using Reto.Application;
using Reto.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfraestructure(builder.Configuration);

var app = builder.Build();
{
    app.UseMiddleware<ExceptionMiddleware>();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
