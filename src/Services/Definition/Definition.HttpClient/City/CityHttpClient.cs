using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.City;

public class CityHttpClient : CareerHttpClient, ICityHttpClient
{
    public CityHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext)
        : base(httpClient, httpContext)
    {
    }

    // api/locations/cities
    public async Task<ConsistentApiResponse<PagedList<CityDto>>> GetAsync(PaginationFilter paginationFilter)
    {
        return await GetAsync<ConsistentApiResponse<PagedList<CityDto>>>(string.Empty, paginationFilter);
    }

    // api/locations/cities/{id}
    public async Task<ConsistentApiResponse<CityDto>> GetByIdAsync(string id)
    {
        return await GetAsync<ConsistentApiResponse<CityDto>>(string.Empty, id);
    }

    // api/locations/cities/{id}/districts
    public async Task<ConsistentApiResponse<PagedList<DistrictDto>>> GetDistrictsOfCityAsync(string cityId, PaginationFilter paginationFilter)
    {
        return await GetAsync<ConsistentApiResponse<PagedList<DistrictDto>>>($"{cityId}/districts", paginationFilter);
    }
}