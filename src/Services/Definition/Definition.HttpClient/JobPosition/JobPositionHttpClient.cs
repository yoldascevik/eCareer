using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.JobPosition;

public class JobPositionHttpClient : CareerHttpClient, IJobPositionHttpClient
{
    public JobPositionHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext)
        : base(httpClient, httpContext)
    {
    }

    // api/v{version}/work/positions
    public async Task<ConsistentApiResponse<PagedList<JobPositionDto>>> GetAsync(PaginationFilter paginationFilter)
    {
        return await GetAsync<ConsistentApiResponse<PagedList<JobPositionDto>>>(string.Empty, paginationFilter);
    }

    // api/v{version}/work/positions/{id}
    public async Task<ConsistentApiResponse<JobPositionDto>> GetByIdAsync(string id)
    {
        return await GetAsync<ConsistentApiResponse<JobPositionDto>>(string.Empty, id);
    }
}