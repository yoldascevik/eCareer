using System;
using Career.Exceptions;
using Career.Http;
using Career.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Company.HttpClient
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCompanyApiHttpClient(this IServiceCollection services, ApiEndpointOptions options)
        {
            Check.NotNull(options, nameof(options));
            
            services.AddCareerHttpClient();
            services.AddSingleton(options);
            services.RegisterModule(new CompanyHttpClientModule());

            return services;
        }
        
        public static IServiceCollection AddCompanyApiHttpClient(this IServiceCollection services, Action<ApiEndpointOptions> options)
        {
            Check.NotNull(options, nameof(options));
            
            var apiEndpointOptions = new ApiEndpointOptions();
            options.Invoke(apiEndpointOptions);

            return services.AddCompanyApiHttpClient(apiEndpointOptions);
        }
    }
}