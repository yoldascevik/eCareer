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
    }
}