using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Http;
using Career.Utilities.Pagination;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.Country
{
    public class CountryHttpClient: CareerHttpClient, ICountryHttpClient
    {
        private readonly string _countryBaseUrl;

        public CountryHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext, ApiEndpointOptions endpointOptions) 
            : base(httpClient, httpContext)
        {
            _countryBaseUrl = $"{endpointOptions.ApiUrl}/api/v{endpointOptions.Version}/location/country";
        }

        // get
        public async Task<ConsistentApiResponse<PagedList<CountryDto>>> GetAsync(PaginationFilter paginationFilter)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<CountryDto>>>(_countryBaseUrl, paginationFilter);
        }

        // {id}
        public async Task<ConsistentApiResponse<CountryDto>> GetByIdAsync(string id)
        {
            return await GetAsync<ConsistentApiResponse<CountryDto>>(_countryBaseUrl, id);
        }

        // code/{code}
        public async Task<ConsistentApiResponse<CountryDto>> GetByCodeAsync(string code)
        {
            return await GetAsync<ConsistentApiResponse<CountryDto>>($"{_countryBaseUrl}/code", code);
        }

        // {countryId}/cities
        public async Task<ConsistentApiResponse<PagedList<CityDto>>> GetCitiesByCountryAsync(string countryId, PaginationFilter paginationFilter)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<CityDto>>>($"{_countryBaseUrl}/{countryId}/cities", paginationFilter);
        }
    }
}