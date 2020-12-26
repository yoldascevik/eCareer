using System;
using Career.Http;
using Career.IoC.IoCModule;
using Company.HttpClient.Company;
using Company.HttpClient.CompanyFollower;
using Microsoft.Extensions.DependencyInjection;

namespace Company.HttpClient
{
    public class CompanyHttpClientModule: IModule
    {
        private readonly ApiEndpointOptions _options;
        
        public CompanyHttpClientModule(ApiEndpointOptions options)
        {
            _options = options;
        }
        
        public void Configure(IServiceCollection services)
        {
            var defaultApiVersion = Version.Parse(_options.DefaultVersion ?? "1.0");
            
            services.AddCareerHttpClient();
            services.AddHttpClientWithRetryPolicy<ICompanyHttpClient, CompanyHttpClient>(config =>
            {
                config.BaseAddress = new Uri($"{_options.ApiUrl}/api/companies/");
                config.DefaultRequestVersion = defaultApiVersion;
            });
            
            services.AddHttpClientWithRetryPolicy<ICompanyFollowerHttpClient, CompanyFollowerHttpClient>(config =>
            {
                config.BaseAddress = new Uri($"{_options.ApiUrl}/api/company-followers/");
                config.DefaultRequestVersion = defaultApiVersion;
            });
        }
    }
}