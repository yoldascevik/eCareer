using System;
using Career.Http;
using Definition.HttpClient.Country;
using Microsoft.Extensions.DependencyInjection;

namespace Definition.HttpClient
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDefinitionApiHttpClient(this IServiceCollection services, ApiEndpointOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            
            services.AddCareerHttpClient();
            services.AddSingleton(options);
            services.AddTransient<ICountryHttpClient, CountryHttpClient>();

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
}