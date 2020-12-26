using System;
using Career.Exceptions;
using Career.Http;
using Company.HttpClient.Company;
using Company.HttpClient.CompanyFollower;
using Microsoft.Extensions.DependencyInjection;

namespace Company.HttpClient
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCompanyApiHttpClient(this IServiceCollection services, ApiEndpointOptions options)
        {
            Check.NotNull(options, nameof(options));
            
            var defaultApiVersion = Version.Parse(options.DefaultVersion ?? "1.0");
            
            services.AddCareerHttpClient();
            services.AddHttpClient<ICompanyHttpClient, CompanyHttpClient>(config =>
            {
                config.BaseAddress = new Uri($"{options.ApiUrl}/api/companies/");
                config.DefaultRequestVersion = defaultApiVersion;
            });
            
            services.AddHttpClient<ICompanyFollowerHttpClient, CompanyFollowerHttpClient>(config =>
            {
                config.BaseAddress = new Uri($"{options.ApiUrl}/api/company-followers/");
                config.DefaultRequestVersion = defaultApiVersion;
            });
            
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