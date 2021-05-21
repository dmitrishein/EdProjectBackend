﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace EdProject.BLL.Common
{
    [ProviderAlias("FileLogger")]
    public class FileLoggerProvider : ILoggerProvider
    {
        public readonly FileLoggerOptions _options;
        public FileLoggerProvider(IOptions<FileLoggerOptions> options)
        {
            _options = options.Value; 
            if(!Directory.Exists(_options.FolderPath))
            {
                Directory.CreateDirectory(_options.FolderPath);
            }
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(this);
        }
        public void Dispose()
        {
        }
    }
}
