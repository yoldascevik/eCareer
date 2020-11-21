using Career.IoC.IoCModule;
using Definition.HttpClient.City;
using Definition.HttpClient.Country;
using Definition.HttpClient.District;
using Definition.HttpClient.EducationLevel;
using Definition.HttpClient.EducationType;
using Definition.HttpClient.JobPosition;
using Definition.HttpClient.Language;
using Definition.HttpClient.ScholarshipType;
using Definition.HttpClient.Sector;
using Definition.HttpClient.WorkType;
using Microsoft.Extensions.DependencyInjection;

namespace Definition.HttpClient
{
    public class DefinitionHttpClientModule : Module
    {
        protected override void Load(IServiceCollection services)
        {
            services.AddTransient<ICityHttpClient, CityHttpClient>();
            services.AddTransient<ICountryHttpClient, CountryHttpClient>();
            services.AddTransient<IDistrictHttpClient, DistrictHttpClient>();
            services.AddTransient<IEducationLevelHttpClient, EducationLevelHttpClient>();
            services.AddTransient<IEducationTypeHttpClient, EducationTypeHttpClient>();
            services.AddTransient<IJobPositionHttpClient, JobPositionHttpClient>();
            services.AddTransient<ILanguageHttpClient, LanguageHttpClient>();
            services.AddTransient<IScholarshipTypeHttpClient, ScholarshipTypeHttpClient>();
            services.AddTransient<ISectorHttpClient, SectorHttpClient>();
            services.AddTransient<IWorkTypeHttpClient, WorkTypeHttpClient>();
        }
    }
}