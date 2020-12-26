using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.City
{
    public class CityHttpClient : CareerHttpClient, ICityHttpClient
    {
        private readonly ApiEndpointOptions _apiEndpointOptions;
        
        public CityHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext, ApiEndpointOptions apiEndpointOptions) 
            : base(httpClient, httpContext)
        {
            _apiEndpointOptions = apiEndpointOptions;
        }
        
        // api/v{version}/locations/cities
        public async Task<ConsistentApiResponse<PagedList<CityDto>>> GetAsync(PaginationFilter paginationFilter, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<CityDto>>>(CreateUrl(null, version), paginationFilter);
        }

        // api/v{version}/locations/cities/{id}
        public async Task<ConsistentApiResponse<CityDto>> GetByIdAsync(string id, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<CityDto>>(CreateUrl(null, version), id);
        }

        // api/v{version}/locations/cities/{id}/districts
        public async Task<ConsistentApiResponse<PagedList<DistrictDto>>> GetDistrictsOfCityAsync(string cityId, PaginationFilter paginationFilter, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<DistrictDto>>>(CreateUrl($"/{cityId}/districts", version), paginationFilter);
        }
        
        private string CreateUrl(string requestPath, string version)
        {
            if (string.IsNullOrEmpty(version))
                version = _apiEndpointOptions.DefaultVersion;

            return $"{_apiEndpointOptions.ApiUrl}/api/v{version}/locations/cities{requestPath ?? string.Empty}";
        }
    }
}