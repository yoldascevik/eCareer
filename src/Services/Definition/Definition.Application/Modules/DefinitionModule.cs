using Career.IoC.IoCModule;
using Definition.Application.Services;
using Definition.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Definition.Application.Modules
{
    public class DefinitionModule: Module
    {
        protected override void Load(IServiceCollection services)
        {
            // Services registration
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IDistrictService, DistrictService>();
        }
    }
}