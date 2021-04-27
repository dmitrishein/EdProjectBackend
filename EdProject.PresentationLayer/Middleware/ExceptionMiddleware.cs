using EdProject.BLL.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace EdProject.PresentationLayer.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger _logger;

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
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync($"{ex.Message}");
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
