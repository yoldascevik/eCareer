using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.JobPosition
{
    public class JobPositionHttpClient: CareerHttpClient, IJobPositionHttpClient
    {
        private readonly ApiEndpointOptions _apiEndpointOptions;
        
        public JobPositionHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext, ApiEndpointOptions apiEndpointOptions) 
            : base(httpClient, httpContext)
        {
            _apiEndpointOptions = apiEndpointOptions;
        }

        // api/v{version}/work/positions"
        public async Task<ConsistentApiResponse<PagedList<JobPositionDto>>> GetAsync(PaginationFilter paginationFilter, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<JobPositionDto>>>(CreateUrl(null, version));
        }

        // api/v{version}/work/positions/{id}";
        public async Task<ConsistentApiResponse<JobPositionDto>> GetByIdAsync(string id, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<JobPositionDto>>(CreateUrl(null, version), id);
        }
        
        private string CreateUrl(string requestPath, string version)
        {
            if (string.IsNullOrEmpty(version))
                version = _apiEndpointOptions.DefaultVersion;

            return $"{_apiEndpointOptions.ApiUrl}/api/v{version}/work/positions{requestPath ?? string.Empty}";
        }
    }
}