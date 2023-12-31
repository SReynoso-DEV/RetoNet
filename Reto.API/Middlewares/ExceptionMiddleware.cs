﻿using Reto.Application.Models;
using Reto.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Reto.API.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                // Invoca el siguiente middleware en la cadena de ejecución
                await next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (BusinessException ex)
            {
                // Manejo de la excepción de negocio y respuesta al cliente
                _logger.LogInformation(ex, "Excepción de negocio.");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var genericResponse = new GenericResponse<object>
                {
                    Status = "error",
                    Message = ex.Message,
                    Data = null
                };

                await context.Response.WriteAsJsonAsync(genericResponse);
            }
            catch (Exception ex)
            {
                // Manejo de la excepción de sistema y respuesta al cliente
                _logger.LogError(ex, "Excepción del sistema.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var genericResponse = new GenericResponse<object>
                {
                    Status = "error",
                    Message = "Ocurrió un error en el sistema.",
                    Data = null
                };

                await context.Response.WriteAsJsonAsync(genericResponse);
            }
        }

        private Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
        {
            _logger.LogError(ex, "Excepción de validacion del sistema.");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var errors = new List<string> { ex.Message };
            var responseBody = new GenericResponse<List<string>>
            {
                Data = errors,
                Message = "Error al procesar la solicitud.",
                Status = "error"
            };

            return context.Response.WriteAsJsonAsync(responseBody);
        }

    }
}
