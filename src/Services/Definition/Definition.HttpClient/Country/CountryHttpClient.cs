using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Http;
using Career.Utilities.Pagination;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.Country
{
    public class CountryHttpClient : CareerHttpClient, ICountryHttpClient
    {
        private readonly ApiEndpointOptions _apiEndpointOptions;

        public CountryHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext, ApiEndpointOptions endpointOptions)
            : base(httpClient, httpContext)
        {
            _apiEndpointOptions = endpointOptions;
        }

        // api/v{version}/location/country";
        public async Task<ConsistentApiResponse<PagedList<CountryDto>>> GetAsync(PaginationFilter paginationFilter, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<CountryDto>>>(CreateUrl(null, version), paginationFilter);
        }

        // api/v{version}/location/country/{id}";
        public async Task<ConsistentApiResponse<CountryDto>> GetByIdAsync(string id, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<CountryDto>>(CreateUrl(null, version), id);
        }

        // api/v{version}/location/country/code/{code}
        public async Task<ConsistentApiResponse<CountryDto>> GetByCodeAsync(string code, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<CountryDto>>(CreateUrl("/code", version), code);
        }

        // api/v{version}/location/country/{countryId}/cities
        public async Task<ConsistentApiResponse<PagedList<CityDto>>> GetCitiesOfCountryAsync(string countryId, PaginationFilter paginationFilter, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<CityDto>>>(CreateUrl($"/{countryId}/cities", version), paginationFilter);
        }

        private string CreateUrl(string requestPath, string version)
        {
            if (string.IsNullOrEmpty(version))
                version = _apiEndpointOptions.DefaultVersion;

            return $"{_apiEndpointOptions.ApiUrl}/api/v{version}/location/country{requestPath ?? string.Empty}";
        }
    }
}