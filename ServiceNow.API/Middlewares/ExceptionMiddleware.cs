using FluentValidation;
using System.Net;
using System.Text.Json;
namespace ServiceNow.ServiceNow.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
              await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(
                 ex,
                 "Validation failed for request {Path}",
                 context.Request.Path);
                    
                await HandleValidationException(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                ex,
                "Unhandled exception while processing {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

                await HandleGeneralException(context, ex);
            }
        }

        public async Task HandleValidationException(HttpContext context, ValidationException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new
            {
                Message = "Validation Failed",

                Errors = ex.Errors.Select(e => new
                {
                    e.PropertyName,
                    e.ErrorMessage
                }
                )
            };
            await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));

        }


        private static async Task HandleGeneralException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

            var response = new
            {
                Message = "Internal Server Error"
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }

}

