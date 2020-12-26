using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.EducationType
{
    public class EducationTypeHttpClient : CareerHttpClient, IEducationTypeHttpClient
    {
        public EducationTypeHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext)
            : base(httpClient, httpContext)
        {
        }

        // api/v{version}/education/types
        public async Task<ConsistentApiResponse<PagedList<EducationTypeDto>>> GetAsync(PaginationFilter paginationFilter)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<EducationTypeDto>>>(string.Empty, paginationFilter);
        }

        // api/v{version}/education/types/{id}
        public async Task<ConsistentApiResponse<EducationTypeDto>> GetByIdAsync(string id)
        {
            return await GetAsync<ConsistentApiResponse<EducationTypeDto>>(string.Empty, id);
        }
    }
}