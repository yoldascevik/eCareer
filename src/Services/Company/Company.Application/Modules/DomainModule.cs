using Career.IoC.IoCModule;
using Company.Application.Company;
using Company.Domain.Rules.Company;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Application.Modules
{
    public class DomainModule: Module
    {
        protected override void Load(IServiceCollection services)
        {
            services.AddTransient<ICompanyTaxNumberUniquenessSpecification, CompanyTaxNumberUniquenessSpecification>();
        }
    }
}