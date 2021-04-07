using EdProject.BLL.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace EdProject.PresentationLayer.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger logger)
        {
            this.next = next;
            this.logger = logger;
        }


        public async Task Invoke(HttpContext httpContext)
        {
            //идучи по контейнеру и сталкиваясь с ошибкой отправляем её в лог
            try
            {
                await next(httpContext);
            }
            catch(Exception ex)
            {
                logger.LogWrite(ex.Message);
            }
        }

    }
}
