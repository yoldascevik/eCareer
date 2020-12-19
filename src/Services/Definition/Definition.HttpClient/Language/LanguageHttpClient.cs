using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.Language
{
    public class LanguageHttpClient: CareerHttpClient, ILanguageHttpClient
    {
        private readonly ApiEndpointOptions _apiEndpointOptions;
        
        public LanguageHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext, ApiEndpointOptions apiEndpointOptions) 
            : base(httpClient, httpContext)
        {
            _apiEndpointOptions = apiEndpointOptions;
        }

        // api/v{version}/languages
        public async Task<ConsistentApiResponse<PagedList<LanguageDto>>> GetAsync(PaginationFilter paginationFilter, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<LanguageDto>>>(CreateUrl(null, version), paginationFilter);
        }

        // api/v{version}/languages/{id}
        public async Task<ConsistentApiResponse<LanguageDto>> GetByIdAsync(string id, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<LanguageDto>>(CreateUrl(null, version), id);
        }

        // api/v{version}/languages/culture/{culture}
        public async Task<ConsistentApiResponse<LanguageDto>> GetByCultureAsync(string culture, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<LanguageDto>>(CreateUrl("/culture", version), culture);
        }

        private string CreateUrl(string requestPath, string version = null)
        {
            if (string.IsNullOrEmpty(version))
                version = _apiEndpointOptions.DefaultVersion;

            return $"{_apiEndpointOptions.ApiUrl}/api/v{version}/languages{requestPath ?? string.Empty}";
        }
    }
}