using EdProject.BLL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
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
            catch (CustomException ex)
            {
                var json = JsonSerializer.Serialize(ex.Errors);
                httpContext.Response.StatusCode = (int)ex.StatusCode;
                await httpContext.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                await httpContext.Response.WriteAsync("Something went wrong! Connect with your administrator");
                _logger.LogError(ex.Message, ex);
            }
            
        }
    }

    public static class ExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
