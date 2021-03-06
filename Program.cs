using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FingerEnglishWebServer.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FingerEnglishWebServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var dbContext = host.Services.GetService(typeof(FingerDbContext));
            DbInitializer.Initialize(dbContext as FingerDbContext);
            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://*:888");
                });
    }
}
