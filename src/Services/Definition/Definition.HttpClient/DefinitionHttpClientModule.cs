using System;
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
        private readonly ApiEndpointOptions _options;
        
        public DefinitionHttpClientModule(ApiEndpointOptions options)
        {
            _options = options;
        }
        
        protected override void Load(IServiceCollection services)
        {
            var defaultApiVersion = Version.Parse(_options.DefaultVersion ?? "1.0");
            
            services.AddHttpClient<ICityHttpClient, CityHttpClient>(config =>
            {
                config.BaseAddress = new Uri($"{_options.ApiUrl}/api/locations/cities/");
                config.DefaultRequestVersion = defaultApiVersion;
            });
            
            services.AddHttpClient<ICountryHttpClient, CountryHttpClient>(config =>
            {
                config.BaseAddress = new Uri($"{_options.ApiUrl}/api/locations/countries/");
                config.DefaultRequestVersion = defaultApiVersion;
            });
            
            services.AddHttpClient<IDistrictHttpClient, DistrictHttpClient>(config =>
            {
                config.BaseAddress = new Uri($"{_options.ApiUrl}/api/locations/districts/");
                config.DefaultRequestVersion = defaultApiVersion;
            });
            
            services.AddHttpClient<IEducationLevelHttpClient, EducationLevelHttpClient>(config =>
            {
                config.BaseAddress = new Uri($"{_options.ApiUrl}/api/education/levels/");
                config.DefaultRequestVersion = defaultApiVersion;
            });
            
            services.AddHttpClient<IEducationTypeHttpClient, EducationTypeHttpClient>(config =>
            {
                config.BaseAddress = new Uri($"{_options.ApiUrl}/api/education/types/");
                config.DefaultRequestVersion = defaultApiVersion;
            });
            
            services.AddHttpClient<IJobPositionHttpClient, JobPositionHttpClient>(config =>
            {
                config.BaseAddress = new Uri($"{_options.ApiUrl}/api/work/positions/");
                config.DefaultRequestVersion = defaultApiVersion;
            });
            
            services.AddHttpClient<ILanguageHttpClient, LanguageHttpClient>(config =>
            {
                config.BaseAddress = new Uri($"{_options.ApiUrl}/api/languages/");
                config.DefaultRequestVersion = defaultApiVersion;
            });
            
            services.AddHttpClient<IScholarshipTypeHttpClient, ScholarshipTypeHttpClient>(config =>
            {
                config.BaseAddress = new Uri($"{_options.ApiUrl}/api/education/scholarshiptypes/");
                config.DefaultRequestVersion = defaultApiVersion;
            });
            
            services.AddHttpClient<ISectorHttpClient, SectorHttpClient>(config =>
            {
                config.BaseAddress = new Uri($"{_options.ApiUrl}/api/work/sectors/");
                config.DefaultRequestVersion = defaultApiVersion;
            });
            
            services.AddHttpClient<IWorkTypeHttpClient, WorkTypeHttpClient>(config =>
            {
                config.BaseAddress = new Uri($"{_options.ApiUrl}/api/work/types/");
                config.DefaultRequestVersion = defaultApiVersion;
            });
        }
    }
}