using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.ScholarshipType
{
    public class ScholarshipTypeHttpClient: CareerHttpClient, IScholarshipTypeHttpClient
    {
        private readonly ApiEndpointOptions _apiEndpointOptions;
        
        public ScholarshipTypeHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext, ApiEndpointOptions apiEndpointOptions) 
            : base(httpClient, httpContext)
        {
            _apiEndpointOptions = apiEndpointOptions;
        }

        // api/v{version}/education/scholarshiptypes
        public async Task<ConsistentApiResponse<PagedList<ScholarshipTypeDto>>> GetAsync(PaginationFilter paginationFilter, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<ScholarshipTypeDto>>>(CreateUrl(null, version));
        }

        // api/v{version}/education/scholarshiptypes/{id}
        public async Task<ConsistentApiResponse<ScholarshipTypeDto>> GetByIdAsync(string id, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<ScholarshipTypeDto>>(CreateUrl(null, version), id);
        }
        
        private string CreateUrl(string requestPath, string version)
        {
            if (string.IsNullOrEmpty(version))
                version = _apiEndpointOptions.DefaultVersion;

            return $"{_apiEndpointOptions.ApiUrl}/api/v{version}/education/scholarshiptypes{requestPath ?? string.Empty}";
        }
    }
}