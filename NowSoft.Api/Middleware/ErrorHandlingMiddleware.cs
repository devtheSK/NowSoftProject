using System.Net;
using System.Text.Json;

namespace NowSoft.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
           
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var result = JsonSerializer.Serialize(new { error = exception.Message });
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return response.WriteAsync(result);
        }
    }
}
