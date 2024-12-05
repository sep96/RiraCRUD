namespace RiraCRUD.Api.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionHandlerMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Custom exceptions from Application layer
            return exception switch
            {
                ValidationException validationEx => HandleValidationException(context, validationEx),
                NotFoundException notFoundEx => HandleNotFoundException(context, notFoundEx),
                UnauthorizedAccessException unauthorizedEx => HandleUnauthorizedException(context, unauthorizedEx),
                _ => HandleUnknownException(context, exception)
            };
        }

        private Task HandleValidationException(HttpContext context, ValidationException exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return context.Response.WriteAsJsonAsync(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Validation failed",
                Errors = exception.Errors
            });
        }

        private Task HandleNotFoundException(HttpContext context, NotFoundException exception)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return context.Response.WriteAsJsonAsync(new
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = exception.Message
            });
        }

        private Task HandleUnauthorizedException(HttpContext context, UnauthorizedAccessException exception)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return context.Response.WriteAsJsonAsync(new
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Message = "Unauthorized access"
            });
        }

        private Task HandleUnknownException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return context.Response.WriteAsJsonAsync(new
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = "An unexpected error occurred"
            });
        }
    }

    // Extension method for easy registration
    public static class GlobalExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }

    // In YourProject.Application/Common/Exceptions/
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IDictionary<string, string[]> failures)
            : this()
        {
            Errors = failures;
        }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
