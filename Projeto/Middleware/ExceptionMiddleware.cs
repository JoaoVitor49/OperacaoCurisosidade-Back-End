using Projeto.Exceptions;
using System.Net;

namespace Projeto.Middleware
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

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (EmailAlreadyExistsException ex)
            {
                _logger.LogError(ex, "Email already exists.");
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation error.");
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "Resource not found.");
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (InvalidCredentialsException ex)
            {
                _logger.LogError(ex, "Invalid credentials.");
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("An unexpected error occurred.");
            }
        }
    }
}
