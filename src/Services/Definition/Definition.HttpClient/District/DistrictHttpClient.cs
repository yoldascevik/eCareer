using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.District;

public class DistrictHttpClient: CareerHttpClient, IDistrictHttpClient
{
    public DistrictHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext) 
        : base(httpClient, httpContext)
    {
            
    }

    // api/v{version}/locations/districts
    public async Task<ConsistentApiResponse<PagedList<DistrictDto>>> GetAsync(PaginationFilter paginationFilter)
    {
        return await GetAsync<ConsistentApiResponse<PagedList<DistrictDto>>>(string.Empty, paginationFilter);
    }

    // api/v{version}/locations/districts/{id}
    public async Task<ConsistentApiResponse<DistrictDto>> GetByIdAsync(string id)
    {
        return await GetAsync<ConsistentApiResponse<DistrictDto>>(string.Empty, id);
    }
}