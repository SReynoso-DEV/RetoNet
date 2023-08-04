using Microsoft.EntityFrameworkCore;
using Reto.API;
using Reto.API.Middlewares;
using Reto.Application;
using Reto.Domain.Interfaces;
using Reto.Infraestructure;
using Reto.Infraestructure.Data;
using Reto.Infraestructure.Repositories;

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
