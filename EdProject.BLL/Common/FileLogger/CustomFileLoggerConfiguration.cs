using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Common.FileLogger
{
    public class CustomFileLoggerConfiguration
    {
        public virtual string LoggerFilename { get; set; }
        public virtual string LoggerPath { get; set; }
    }
}
