using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Http;
using Career.Utilities.Pagination;
using Definition.Contract.Dto;

namespace Definition.HttpClient.City
{
    public interface ICityHttpClient : ICareerHttpClient
    {
        Task<ConsistentApiResponse<PagedList<CityDto>>> GetAsync(PaginationFilter paginationFilter, string version);
        
        Task<ConsistentApiResponse<CityDto>> GetByIdAsync(string id, string version);
        
        Task<ConsistentApiResponse<PagedList<DistrictDto>>> GetDistrictsOfCityAsync(string cityId, PaginationFilter paginationFilter, string version = null);
    }
}