using LibraryAPI.Common.Response;
using System.Net;
using System.Text.Json;

namespace LibraryAPI.Middleware
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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var responseObject = new ResponseObject
                {
                    Message = "An unexpected error occurred."
                };

                var errorJson = JsonSerializer.Serialize(responseObject);

                await context.Response.WriteAsync(errorJson);
            }
        }
    }
}
