using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Career.Mvc.Extensions;

public static class ApiVersionExtension
{
    /// <summary>
    /// Add api versiyon
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="apiVersionReader">If null then will be use HeaderApiVersionReader with "X-Api-Version" key.</param>
    /// <returns></returns>
    public static IServiceCollection AddApiVersion(this IServiceCollection services, IApiVersionReader apiVersionReader = null)
        => services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
            config.ApiVersionReader = apiVersionReader ?? new HeaderApiVersionReader("X-Api-Version");
        });
}