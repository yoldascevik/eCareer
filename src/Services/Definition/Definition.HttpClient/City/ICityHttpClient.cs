using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;

namespace Definition.HttpClient.City;

public interface ICityHttpClient : ICareerHttpClient
{
    Task<ConsistentApiResponse<PagedList<CityDto>>> GetAsync(PaginationFilter paginationFilter);
        
    Task<ConsistentApiResponse<CityDto>> GetByIdAsync(string id);
        
    Task<ConsistentApiResponse<PagedList<DistrictDto>>> GetDistrictsOfCityAsync(string cityId, PaginationFilter paginationFilter);
}