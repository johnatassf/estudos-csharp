using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace EstudoAutenticacao
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;
                var path = Directory.GetCurrentDirectory();

                //config.SetBasePath(path)
                //      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                //      .AddJsonFile($"appsettings{env}.json", optional: true, reloadOnChange: true)
                //      .AddEnvironmentVariables();
            })

            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
