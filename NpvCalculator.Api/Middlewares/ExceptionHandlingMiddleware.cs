using System.Net;
using FluentValidation;
using NpvCalculator.Core.Common;

namespace NpvCalculator.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation error occurred.");
                await HandleValidationExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var errors = exception.Errors.Select(err => new ValidationError
            {
                PropertyName = err.PropertyName,
                ErrorMessage = err.ErrorMessage
            });

            return context.Response.WriteAsJsonAsync(new ValidationErrorResponse
            {
                Message = "Validation failed",
                Errors = errors.ToList()
            });
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsJsonAsync(new { Message = "An error occurred", Details = exception.Message });
        }
    }

}
