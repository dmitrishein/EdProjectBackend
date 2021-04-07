using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EdProject.BLL.Common.Interfaces
{
    public interface ILogger 
    {
        public void Log(string logMessage, TextWriter textWriter);
        public void LogWrite(string logMessage);
    }
}
