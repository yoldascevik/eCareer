using AutoMapper;
using Career.Configuration;
using Career.EntityFramework;
using Career.IoC.IoCModule;
using Company.Application.Mapping;
using Company.Application.Services;
using Company.Application.Services.Abstractions;
using Company.Domain.Repository;
using Company.Infrastructure;
using Company.Infrastructure.Repositories;
using Definition.HttpClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(IServiceCollection services)
        {
            IConfiguration configuration = ConfigurationHelper.GetConfiguration();
            var definitionApiEndPoint = configuration.GetSection("ApiEndpoints:DefinitionApi").Get<ApiEndpointOptions>();
            
            services.AddDefinitionApiHttpClient(definitionApiEndPoint);

            services.AddDbContext<CompanyDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("CompanyDatabase")));
            services.AddUnitOfWork<CompanyDbContext>();

            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ISectorService, SectorService>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyFollowerRepository, CompanyFollowerRepository>();

            services.AddAutoMapper(typeof(CompanyMappinProfile));
        }
    }
}