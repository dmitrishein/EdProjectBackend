using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace EdProject.BLL.Common.FileLogger
{
    public class CustomFileLogger:ILogger
    {
        private readonly string _name;
        private readonly CustomFileLoggerConfiguration _config;

        public CustomFileLogger(string name, CustomFileLoggerConfiguration config)
        {
            (_name, _config) = (name, config);
            if(!Directory.Exists(_config.LoggerPath))
            {
                Directory.CreateDirectory(_config.LoggerPath);
            }
        }
        public IDisposable BeginScope<TState>(TState state) => default;
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if(!IsEnabled(logLevel))
            {
                return;
            }

            var environmentPath = Directory.GetCurrentDirectory();/*$"{_config.LoggerPath}/{_config.LoggerFilename.Replace("{date}", DateTimeOffset.UtcNow.ToString("d"))}";*/
            var loggerFilename = String.Format("log_{0}", DateTime.Now.ToString("d"));
            var fullFilePath = String.Format("{0}\\{1}.txt", environmentPath, loggerFilename);
            var stack = exception is not null ? exception.StackTrace : string.Empty;
            
            var resEx = exception is not null  ? formatter(state, exception):string.Empty;
            var logRecord = $"[{DateTimeOffset.UtcNow}] {logLevel}, {resEx}, {stack}";

            using (var streamWriter = new StreamWriter(fullFilePath, true))
            {
                streamWriter.WriteLine(logRecord);
            }
        }
    }
}
