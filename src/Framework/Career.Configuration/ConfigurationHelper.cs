using Microsoft.Extensions.Configuration;

namespace Career.Configuration;

public static class ConfigurationHelper
{
    public static IConfiguration GetConfiguration()
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