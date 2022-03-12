namespace Gateway.Web.Api.Helpers;

public static class ConfigurationHelper
{
    public static IConfiguration GetConfiguration()
    {
        string? environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{environmentName}.json", true)
            .AddJsonFile($"configuration.{environmentName}.json")
            .AddEnvironmentVariables();

        return builder.Build();
    }
}