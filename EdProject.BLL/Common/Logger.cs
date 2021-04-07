using EdProject.BLL.Common.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace EdProject.BLL.Common
{
    public class Logger : ILogger
    {
        
        private string _executionPath = string.Empty;
        public Logger(string logMessage)
        {
            LogWrite(logMessage);
        }

        public void LogWrite(string logMessage)
        {
            //получаем путь к выполняемой сборке
            _executionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //записываем сообщение в файл
            try
            {
                using (StreamWriter w = File.AppendText(_executionPath + "\\" + "log.txt"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception) { }
        }

        public void Log(string logMessage,TextWriter textWriter)
        {
            try
            {
                textWriter.Write("\r\nLog Entry : ");
                textWriter.WriteLine("{0}, {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                textWriter.WriteLine(" :{0}", logMessage);
                textWriter.WriteLine("------------------------");
            }
            catch (Exception) { }
            
        }
    }
}
