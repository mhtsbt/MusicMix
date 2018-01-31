using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Server.Kestrel;
using System.IO;

namespace MusicMix
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {

            var builder = new WebHostBuilder()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseKestrel()
             .ConfigureAppConfiguration((hostingContext, config) =>
             {
                 var env = hostingContext.HostingEnvironment;
                 config.AddJsonFile(Path.Combine("appsettings.json"), optional: true, reloadOnChange: true)
                       .AddJsonFile(Path.Combine($"appsettings.{env.EnvironmentName.ToLower()}.json"), optional: false, reloadOnChange: true);
                 config.AddEnvironmentVariables();
             })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddDebug();
            }).UseStartup<Startup>()
              .UseUrls("http://*:5000")
              .Build();

            return builder;

        }
    }
}
