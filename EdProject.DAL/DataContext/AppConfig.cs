using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EdProject.DAL.DataContext
{
    public class AppConfig
    {
        public AppConfig()
        {   //Добавить считывание с appsettings строки
            var configBuilder = new ConfigurationBuilder();
            var root = configBuilder.Build();
            sqlConnectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=EdProjectDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
        public string sqlConnectionString { get; set; }


    }
}
