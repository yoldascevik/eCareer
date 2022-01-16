using Career.Http;
using Career.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Definition.HttpClient;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDefinitionApiHttpClient(this IServiceCollection services, ApiEndpointOptions options)
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options));
            
        services.AddCareerHttpClient();
        services.RegisterModule(new DefinitionHttpClientModule(options));

        return services;
    }
        
    public static IServiceCollection AddDefinitionApiHttpClient(this IServiceCollection services, Action<ApiEndpointOptions> options)
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options));
            
        var apiEndpointOptions = new ApiEndpointOptions();
        options.Invoke(apiEndpointOptions);

        return services.AddDefinitionApiHttpClient(apiEndpointOptions);
    }
}