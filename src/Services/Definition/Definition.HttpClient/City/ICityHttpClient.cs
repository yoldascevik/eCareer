using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http;
using Definition.Contract.Dto;

namespace Definition.HttpClient.City
{
    public interface ICityHttpClient : ICareerHttpClient
    {
        Task<ConsistentApiResponse<PagedList<CityDto>>> GetAsync(PaginationFilter paginationFilter, string version = null);
        
        Task<ConsistentApiResponse<CityDto>> GetByIdAsync(string id, string version = null);
        
        Task<ConsistentApiResponse<PagedList<DistrictDto>>> GetDistrictsOfCityAsync(string cityId, PaginationFilter paginationFilter, string version = null);
    }
}