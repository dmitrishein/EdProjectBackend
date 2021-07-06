using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EdProject.PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging((HostBuilderContext, logging) =>
                {
                    logging.AddCustomFileLogger(options => {
                        HostBuilderContext.Configuration.GetSection("Logging").GetSection("FileLogger").GetSection("Options").Bind(options);
                    });
                });
                

    }
}
