using Career.IoC.IoCModule;
using Company.Application.Company;
using Company.Domain.Rules;
using Company.Domain.Rules.Company;
using Company.Domain.Rules.CompanyAddress;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Application
{
    public class DomainModule: Module
    {
        protected override void Load(IServiceCollection services)
        {
            services.AddTransient<ICompanyTaxNumberUniquenessSpecification, CompanyTaxNumberUniquenessSpecification>();
            services.AddTransient<IValidAddressSpecification>(); //TODO
        }
    }
}