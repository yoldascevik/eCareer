using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.District
{
    public class DistrictHttpClient: CareerHttpClient, IDistrictHttpClient
    {
        private readonly ApiEndpointOptions _apiEndpointOptions;
        
        public DistrictHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext, ApiEndpointOptions apiEndpointOptions) 
            : base(httpClient, httpContext)
        {
            _apiEndpointOptions = apiEndpointOptions;
        }

        // api/v{version}/locations/districts
        public async Task<ConsistentApiResponse<PagedList<DistrictDto>>> GetAsync(PaginationFilter paginationFilter, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<DistrictDto>>>(CreateUrl(null, version), paginationFilter);
        }

        // api/v{version}/locations/districts/{id}
        public async Task<ConsistentApiResponse<DistrictDto>> GetByIdAsync(string id, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<DistrictDto>>(CreateUrl(null, version), id);
        }
        
        private string CreateUrl(string requestPath, string version)
        {
            if (string.IsNullOrEmpty(version))
                version = _apiEndpointOptions.DefaultVersion;

            return $"{_apiEndpointOptions.ApiUrl}/api/v{version}/locations/districts{requestPath ?? string.Empty}";
        }
    }
}