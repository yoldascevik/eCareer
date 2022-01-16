using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.EducationLevel;

public class EducationLevelHttpClient: CareerHttpClient, IEducationLevelHttpClient
{
    public EducationLevelHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext) 
        : base(httpClient, httpContext)
    {
    }

    // api/v{version}/education/levels
    public async Task<ConsistentApiResponse<PagedList<EducationLevelDto>>> GetAsync(PaginationFilter paginationFilter) 
    {
        return await GetAsync<ConsistentApiResponse<PagedList<EducationLevelDto>>>(string.Empty, paginationFilter);
    }

    // api/v{version}/education/levels/{id}
    public async Task<ConsistentApiResponse<EducationLevelDto>> GetByIdAsync(string id)
    {
        return await GetAsync<ConsistentApiResponse<EducationLevelDto>>(string.Empty, id);
    }
}