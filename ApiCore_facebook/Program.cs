using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiCore_facebook.ClassController.log;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApiCore_facebook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();

            //Logiing
            //var webHost = new WebHostBuilder()
            //    .UseKestrel()
            //    .UseContentRoot(Directory.GetCurrentDirectory())
            //    .ConfigureAppConfiguration((hostingContext, config) =>
            //    {
            //        var env = hostingContext.HostingEnvironment;
            //        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //              .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
            //                  optional: true, reloadOnChange: true);
            //        config.AddEnvironmentVariables();
            //    })
            //    .ConfigureLogging((hostingContext, logging) =>
            //    {
            //        // Requires `using Microsoft.Extensions.Logging;`
            //        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
            //        logging.AddConsole();
            //        logging.AddDebug();
            //        logging.AddEventSourceLogger();
            //    })
            //    .UseStartup<Startup>()
            //    .Build();

            var host = CreateWebHostBuilder(args).Build();

            //var logRepository = host.Services.GetRequiredService<ILogRepository>();
            //logRepository.Add(new Logitem() { Name = "Feed the dog" });
            //logRepository.Add(new Logitem() { Name = "Walk the dog" });

            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Seeded the database.");

            host.Run();
            //webHost.Run();
        }
        /// <summary>
        /// Build ứng dụng trở thành 1 web service
        /// Bắt đầu chạy file stasup
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
               .ConfigureLogging((logging) =>
                {
                    
                    logging.AddEventSourceLogger();
                    logging.AddConsole();
                    logging.AddDebug();
                }).UseStartup<Startup>();
    }
}
