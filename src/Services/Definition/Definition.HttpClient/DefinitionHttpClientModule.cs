using Career.Http;
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

namespace Definition.HttpClient;

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
            
        services.AddHttpClientWithRetryPolicy<ICityHttpClient, CityHttpClient>(config =>
        {
            config.BaseAddress = new Uri($"{_options.ApiUrl}/api/locations/cities/");
            config.DefaultRequestVersion = defaultApiVersion;
        });
            
        services.AddHttpClientWithRetryPolicy<ICountryHttpClient, CountryHttpClient>(config =>
        {
            config.BaseAddress = new Uri($"{_options.ApiUrl}/api/locations/countries/");
            config.DefaultRequestVersion = defaultApiVersion;
        });
            
        services.AddHttpClientWithRetryPolicy<IDistrictHttpClient, DistrictHttpClient>(config =>
        {
            config.BaseAddress = new Uri($"{_options.ApiUrl}/api/locations/districts/");
            config.DefaultRequestVersion = defaultApiVersion;
        });
            
        services.AddHttpClientWithRetryPolicy<IEducationLevelHttpClient, EducationLevelHttpClient>(config =>
        {
            config.BaseAddress = new Uri($"{_options.ApiUrl}/api/education/levels/");
            config.DefaultRequestVersion = defaultApiVersion;
        });
            
        services.AddHttpClientWithRetryPolicy<IEducationTypeHttpClient, EducationTypeHttpClient>(config =>
        {
            config.BaseAddress = new Uri($"{_options.ApiUrl}/api/education/types/");
            config.DefaultRequestVersion = defaultApiVersion;
        });
            
        services.AddHttpClientWithRetryPolicy<IJobPositionHttpClient, JobPositionHttpClient>(config =>
        {
            config.BaseAddress = new Uri($"{_options.ApiUrl}/api/work/positions/");
            config.DefaultRequestVersion = defaultApiVersion;
        });
            
        services.AddHttpClientWithRetryPolicy<ILanguageHttpClient, LanguageHttpClient>(config =>
        {
            config.BaseAddress = new Uri($"{_options.ApiUrl}/api/languages/");
            config.DefaultRequestVersion = defaultApiVersion;
        });
            
        services.AddHttpClientWithRetryPolicy<IScholarshipTypeHttpClient, ScholarshipTypeHttpClient>(config =>
        {
            config.BaseAddress = new Uri($"{_options.ApiUrl}/api/education/scholarshiptypes/");
            config.DefaultRequestVersion = defaultApiVersion;
        });
            
        services.AddHttpClientWithRetryPolicy<ISectorHttpClient, SectorHttpClient>(config =>
        {
            config.BaseAddress = new Uri($"{_options.ApiUrl}/api/work/sectors/");
            config.DefaultRequestVersion = defaultApiVersion;
        });
            
        services.AddHttpClientWithRetryPolicy<IWorkTypeHttpClient, WorkTypeHttpClient>(config =>
        {
            config.BaseAddress = new Uri($"{_options.ApiUrl}/api/work/types/");
            config.DefaultRequestVersion = defaultApiVersion;
        });
    }
}