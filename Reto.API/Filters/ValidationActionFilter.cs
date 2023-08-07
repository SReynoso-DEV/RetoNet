using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Reto.Application.Models;

namespace Reto.API.Filters
{
    public class ValidationActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = new List<string>();

                foreach (var value in context.ModelState.Values)
                {
                    errors.AddRange(value.Errors.Select(error => error.ErrorMessage));
                }

                var response = new GenericResponse<List<string>>
                {
                    Data = errors,
                    Status = "Error",
                    Message = "Error de validacion al procesar la solicitud."
                };

                context.Result = new BadRequestObjectResult(response);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No es necesario implementar nada aquí
        }
    }
}
