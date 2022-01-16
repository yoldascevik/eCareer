using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.ScholarshipType;

public class ScholarshipTypeHttpClient: CareerHttpClient, IScholarshipTypeHttpClient
{
    public ScholarshipTypeHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext) 
        : base(httpClient, httpContext)
    {
    }

    // api/v{version}/education/scholarshiptypes
    public async Task<ConsistentApiResponse<PagedList<ScholarshipTypeDto>>> GetAsync(PaginationFilter paginationFilter)
    {
        return await GetAsync<ConsistentApiResponse<PagedList<ScholarshipTypeDto>>>(string.Empty, paginationFilter);
    }

    // api/v{version}/education/scholarshiptypes/{id}
    public async Task<ConsistentApiResponse<ScholarshipTypeDto>> GetByIdAsync(string id)
    {
        return await GetAsync<ConsistentApiResponse<ScholarshipTypeDto>>(string.Empty, id);
    }
}