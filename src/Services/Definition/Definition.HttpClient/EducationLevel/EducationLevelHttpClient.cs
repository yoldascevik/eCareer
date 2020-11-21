using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Http;
using Career.Utilities.Pagination;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.EducationLevel
{
    public class EducationLevelHttpClient: CareerHttpClient, IEducationLevelHttpClient
    {
        private readonly ApiEndpointOptions _apiEndpointOptions;
        
        public EducationLevelHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext, ApiEndpointOptions apiEndpointOptions) 
            : base(httpClient, httpContext)
        {
            _apiEndpointOptions = apiEndpointOptions;
        }

        // api/v{version}/education/levels"
        public async Task<ConsistentApiResponse<PagedList<EducationLevelDto>>> GetAsync(PaginationFilter paginationFilter, string version)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<EducationLevelDto>>>(CreateUrl(null, version));
        }

        // api/v{version}/education/levels/{id}";
        public async Task<ConsistentApiResponse<EducationLevelDto>> GetByIdAsync(string id, string version)
        {
            return await GetAsync<ConsistentApiResponse<EducationLevelDto>>(CreateUrl(null, version), id);
        }
        
        private string CreateUrl(string requestPath, string version)
        {
            if (string.IsNullOrEmpty(version))
                version = _apiEndpointOptions.DefaultVersion;

            return $"{_apiEndpointOptions.ApiUrl}/api/v{version}/education/levels{requestPath ?? string.Empty}";
        }
    }
}