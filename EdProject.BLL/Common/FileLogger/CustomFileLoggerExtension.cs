using EdProject.BLL.Common.FileLogger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace EdProject
{
    public static class CustomFileLoggerExtension
    {
        public static ILoggingBuilder AddCustomFileLogger(this ILoggingBuilder builder,Action<CustomFileLoggerConfiguration> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, CustomFileLoggerProvider>();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}
