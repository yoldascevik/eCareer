using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.WorkType
{
    public class WorkTypeHttpClient: CareerHttpClient, IWorkTypeHttpClient
    {
        private readonly ApiEndpointOptions _apiEndpointOptions;
        
        public WorkTypeHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext, ApiEndpointOptions apiEndpointOptions) 
            : base(httpClient, httpContext)
        {
            _apiEndpointOptions = apiEndpointOptions;
        }

        // api/v{version}/work/types
        public async Task<ConsistentApiResponse<PagedList<WorkTypeDto>>> GetAsync(PaginationFilter paginationFilter, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<WorkTypeDto>>>(CreateUrl(null, version));
        }

        // api/v{version}/work/types/{id}
        public async Task<ConsistentApiResponse<WorkTypeDto>> GetByIdAsync(string id, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<WorkTypeDto>>(CreateUrl(null, version), id);
        }
        
        private string CreateUrl(string requestPath, string version)
        {
            if (string.IsNullOrEmpty(version))
                version = _apiEndpointOptions.DefaultVersion;

            return $"{_apiEndpointOptions.ApiUrl}/api/v{version}/work/types{requestPath ?? string.Empty}";
        }
    }
}