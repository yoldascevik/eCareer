using Microsoft.Extensions.Configuration;

namespace Career.Swagger.Helpers;

internal class ConfigurationHelper
{
    internal static SwaggerOptions GetSwaggerOptions(IConfiguration configuration)
    {
        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));

        var sectionName = nameof(SwaggerOptions);
        var configurationSection = configuration.GetSection(sectionName);
        if (configurationSection == null)
            throw new FileLoadException($"{sectionName} section not found in appsettings!");
            
        var swaggerOptions = new SwaggerOptions();
        configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

        return swaggerOptions;
    }
}