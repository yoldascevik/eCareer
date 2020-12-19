using Career.IoC.IoCModule;
using Company.HttpClient.Company;
using Microsoft.Extensions.DependencyInjection;

namespace Company.HttpClient
{
    public class CompanyHttpClientModule : Module
    {
        protected override void Load(IServiceCollection services)
        {
            services.AddTransient<ICompanyHttpClient, CompanyHttpClient>();
            // services.AddTransient<ICompanyFollowerHttpClient, CompanyFollowerHttpClient>();
        }
    }
}