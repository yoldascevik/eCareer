using Career.IoC.IoCModule;
using Definition.Application.Education.EducationLevel;
using Definition.Application.Education.EducationType;
using Definition.Application.Language;
using Definition.Application.Location.City;
using Definition.Application.Location.Country;
using Definition.Application.Location.District;
using Microsoft.Extensions.DependencyInjection;

namespace Definition.Application
{
    public class DefinitionModule: Module
    {
        protected override void Load(IServiceCollection services)
        {
            // Services registration
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IDistrictService, DistrictService>();
            
            services.AddScoped<IEducationLevelService, EducationLevelService>();
            services.AddScoped<IEducationTypeService, EducationTypeService>();
            
            services.AddScoped<ILanguageService, LanguageService>();
        }
    }
}