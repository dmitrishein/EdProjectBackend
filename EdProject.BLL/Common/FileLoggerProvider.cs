﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }
    }
}
