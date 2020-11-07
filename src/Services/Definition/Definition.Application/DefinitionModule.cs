using Career.IoC.IoCModule;
using Definition.Application.Education.EducationLevel;
using Definition.Application.Education.EducationType;
using Definition.Application.Education.ScholarshipType;
using Definition.Application.Language;
using Definition.Application.Location.City;
using Definition.Application.Location.Country;
using Definition.Application.Location.District;
using Definition.Application.Work.Sector;
using Definition.Application.Work.WorkType;
using Microsoft.Extensions.DependencyInjection;

namespace Definition.Application
{
    public class DefinitionModule: Module
    {
        protected override void Load(IServiceCollection services)
        {
            // location services
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IDistrictService, DistrictService>();
            
            // education services
            services.AddScoped<IEducationLevelService, EducationLevelService>();
            services.AddScoped<IEducationTypeService, EducationTypeService>();
            services.AddScoped<IScholarshipTypeService, ScholarshipTypeService>();
            
            // work services
            services.AddScoped<ISectorService, SectorService>();
            services.AddScoped<IWorkTypeService, WorkTypeService>();

            // other
            services.AddScoped<ILanguageService, LanguageService>();
        }
    }
}