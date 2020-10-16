using Microsoft.Extensions.Configuration;
using Serilog;

namespace Logging
{
    public class CareerSerilogLoggerFactory
    {
        public static ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            string applicationName = configuration["Serilog:ApplicationName"];
            
            return new LoggerConfiguration()
                .Enrich.WithProperty("ApplicationContext", applicationName)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}