using System;
using Career.Configuration;
using Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Job.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = ConfigurationHelper.GetConfiguration();
            Log.Logger = CareerSerilogLoggerFactory.CreateSerilogLogger(configuration);

            try
            {
                Log.Information("Application starting up...");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application failed to start correctly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}