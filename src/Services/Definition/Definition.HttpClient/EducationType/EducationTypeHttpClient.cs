using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.EducationType
{
    public class EducationTypeHttpClient: CareerHttpClient, IEducationTypeHttpClient
    {
        private readonly ApiEndpointOptions _apiEndpointOptions;
        
        public EducationTypeHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext, ApiEndpointOptions apiEndpointOptions) 
            : base(httpClient, httpContext)
        {
            _apiEndpointOptions = apiEndpointOptions;
        }

        // api/v{version}/education/types
        public async Task<ConsistentApiResponse<PagedList<EducationTypeDto>>> GetAsync(PaginationFilter paginationFilter, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<EducationTypeDto>>>(CreateUrl(null, version), paginationFilter);
        }

        // api/v{version}/education/types/{id}
        public async Task<ConsistentApiResponse<EducationTypeDto>> GetByIdAsync(string id, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<EducationTypeDto>>(CreateUrl(null, version), id);
        }
        
        private string CreateUrl(string requestPath, string version)
        {
            if (string.IsNullOrEmpty(version))
                version = _apiEndpointOptions.DefaultVersion;

            return $"{_apiEndpointOptions.ApiUrl}/api/v{version}/education/types{requestPath ?? string.Empty}";
        }
    }
}