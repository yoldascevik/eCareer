using AutoMapper;
using Career.Configuration;
using Career.IoC.IoCModule;
using Career.MediatR;
using Career.Shared.OS;
using Company.Application.Specifications.Company;
using Company.Domain.Repository;
using Company.Infrastructure;
using Definition.HttpClient;
using FluentValidation;
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
            
            services.AddDateTimeProvider();
            services.AddDefinitionApiHttpClient(definitionApiEndPoint);
            services.AddValidatorsFromAssembly(typeof(ApplicationModule).Assembly);
            services.AddMediatRPipelineBehavior();
            
            services.AddScoped<ICompanySectorSpecification, CompanySectorSpecification>();
            services.AddScoped<ICompanyLocationSpecification, CompanyLocationSpecification>();
            services.AddScoped<ICompanyTaxNumberSpecification, CompanyTaxNumberSpecification>();

            services.AddAutoMapper(typeof(CompanyMappinProfile));
            services.AddScoped<ICompanyRepository, CompanyRepository>();
        }
    }
}