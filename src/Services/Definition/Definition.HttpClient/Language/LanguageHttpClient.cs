using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.Language
{
    public class LanguageHttpClient : CareerHttpClient, ILanguageHttpClient
    {
        public LanguageHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext)
            : base(httpClient, httpContext)
        {
        }

        // api/v{version}/languages
        public async Task<ConsistentApiResponse<PagedList<LanguageDto>>> GetAsync(PaginationFilter paginationFilter)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<LanguageDto>>>(string.Empty, paginationFilter);
        }

        // api/v{version}/languages/{id}
        public async Task<ConsistentApiResponse<LanguageDto>> GetByIdAsync(string id)
        {
            return await GetAsync<ConsistentApiResponse<LanguageDto>>(string.Empty, id);
        }

        // api/v{version}/languages/culture/{culture}
        public async Task<ConsistentApiResponse<LanguageDto>> GetByCultureAsync(string culture)
        {
            return await GetAsync<ConsistentApiResponse<LanguageDto>>("culture", culture);
        }
    }
}