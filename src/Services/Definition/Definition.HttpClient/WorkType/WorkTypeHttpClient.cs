using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.WorkType
{
    public class WorkTypeHttpClient: CareerHttpClient, IWorkTypeHttpClient
    {
        public WorkTypeHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext) 
            : base(httpClient, httpContext)
        {
        }

        // api/v{version}/work/types
        public async Task<ConsistentApiResponse<PagedList<WorkTypeDto>>> GetAsync(PaginationFilter paginationFilter)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<WorkTypeDto>>>(string.Empty, paginationFilter);
        }

        // api/v{version}/work/types/{id}
        public async Task<ConsistentApiResponse<WorkTypeDto>> GetByIdAsync(string id)
        {
            return await GetAsync<ConsistentApiResponse<WorkTypeDto>>(string.Empty, id);
        }
    }
}