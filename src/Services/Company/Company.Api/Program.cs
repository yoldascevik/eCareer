using System;
using System.IO;
using Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Company.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();
            Log.Logger = CareerSerilogLoggerFactory.CreateSerilogLogger(configuration);

            try
            {
                Log.Information("Application starting up...");

                CreateHostBuilder(args).Build() .Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application failed to start correctly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        
        private static IConfiguration GetConfiguration()
        {
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}