using AutoMapper;
using Career.Configuration;
using Career.EntityFramework;
using Career.IoC;
using Career.IoC.IoCModule;
using Career.Shared.System.DateTimeProvider;
using Company.Application.Company;
using Company.Application.Company.Services;
using Company.Domain.Repositories;
using Company.Infrastructure;
using Company.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Application.Modules
{
    public class ApplicationModule : Module
    {
        protected override void Load(IServiceCollection services)
        {
            IConfiguration configuration = ConfigurationHelper.GetConfiguration();

            services.AddDbContext<CompanyDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("CompanyDatabase")));
            services.AddUnitOfWork<CompanyDbContext>();
            
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyFollowerRepository, CompanyFollowerRepository>();

            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            
            services.AddAutoMapper(typeof(CompanyMappinProfile));
            services.RegisterModule(new DomainModule());
        }
    }
}