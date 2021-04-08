using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;

namespace EdProject.BLL.Common
{
    public class FileLogger : ILogger
    {
        
        private string _pathToLogFile;
        private static object _lock = new object();

        public FileLogger(string path)
        {
            _pathToLogFile = path;
        }


        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if(formatter != null  )
            {
                lock(_lock)
                {
                    File.AppendAllText(_pathToLogFile, formatter(state, exception) + Environment.NewLine);
                }
            }
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
  
    }

}

