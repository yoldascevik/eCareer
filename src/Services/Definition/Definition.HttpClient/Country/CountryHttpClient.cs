using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.Country
{
    public class CountryHttpClient : CareerHttpClient, ICountryHttpClient
    {
        public CountryHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext)
            : base(httpClient, httpContext)
        {
        }

        // api/v{version}/locations/countries
        public async Task<ConsistentApiResponse<PagedList<CountryDto>>> GetAsync(PaginationFilter paginationFilter)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<CountryDto>>>(string.Empty, paginationFilter);
        }

        // api/v{version}/locations/countries/{id}
        public async Task<ConsistentApiResponse<CountryDto>> GetByIdAsync(string id)
        {
            return await GetAsync<ConsistentApiResponse<CountryDto>>(string.Empty, id);
        }

        // api/v{version}/locations/countries/code/{code}
        public async Task<ConsistentApiResponse<CountryDto>> GetByCodeAsync(string code)
        {
            return await GetAsync<ConsistentApiResponse<CountryDto>>("code", code);
        }

        // api/v{version}/locations/countries/{countryId}/cities
        public async Task<ConsistentApiResponse<PagedList<CityDto>>> GetCitiesOfCountryAsync(string countryId, PaginationFilter paginationFilter)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<CityDto>>>($"{countryId}/cities", paginationFilter);
        }
    }
}