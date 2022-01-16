using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.Sector;

public class SectorHttpClient: CareerHttpClient, ISectorHttpClient
{
    public SectorHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext) 
        : base(httpClient, httpContext)
    {
    }

    // api/v{version}/work/sectors
    public async Task<ConsistentApiResponse<PagedList<SectorDto>>> GetAsync(PaginationFilter paginationFilter)
    {
        return await GetAsync<ConsistentApiResponse<PagedList<SectorDto>>>(string.Empty, paginationFilter);
    }

    // api/v{version}/work/sectors/{id}/positions
    public async Task<ConsistentApiResponse<PagedList<JobPositionDto>>> GetJobPositionsOfSector(string sectorId, PaginationFilter paginationFilter)
    {
        return await GetAsync<ConsistentApiResponse<PagedList<JobPositionDto>>>($"{sectorId}/positions", paginationFilter);
    }

    // api/v{version}/work/sectors/{id}
    public async Task<ConsistentApiResponse<SectorDto>> GetByIdAsync(string id)
    {
        return await GetAsync<ConsistentApiResponse<SectorDto>>(string.Empty, id);
    }
}