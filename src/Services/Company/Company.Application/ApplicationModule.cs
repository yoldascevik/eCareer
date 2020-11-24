using AutoMapper;
using Career.Configuration;
using Career.IoC.IoCModule;
using Career.MediatR;
using Company.Application.Services;
using Company.Application.Services.Abstractions;
using Company.Domain.Repository;
using Company.Infrastructure;
using Definition.HttpClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Application
{
    public class ApplicationModule: Module
    {
        protected override void Load(IServiceCollection services)
        {
            IConfiguration configuration = ConfigurationHelper.GetConfiguration();
            var definitionApiEndPoint = configuration.GetSection("ApiEndpoints:DefinitionApi").Get<ApiEndpointOptions>();
            
            services.AddDefinitionApiHttpClient(definitionApiEndPoint);
            services.AddMediatRWithFluentValidation(typeof(ApplicationModule));

            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<ISectorService, SectorService>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddAutoMapper(typeof(CompanyMappinProfile));
        }
    }
}