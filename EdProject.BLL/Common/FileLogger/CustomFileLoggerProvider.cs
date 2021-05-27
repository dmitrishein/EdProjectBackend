using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace EdProject.BLL.Common.FileLogger
{
    public class CustomFileLoggerProvider : ILoggerProvider
    {
        private readonly IDisposable _onChangeToken;
        private CustomFileLoggerConfiguration _currentConfig;

        public CustomFileLoggerProvider(IOptionsMonitor<CustomFileLoggerConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new CustomFileLogger(categoryName,_currentConfig);
        }

        public void Dispose()
        {
            _onChangeToken.Dispose();
        }
    }
}
