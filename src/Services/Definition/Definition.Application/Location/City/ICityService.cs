using System.Threading.Tasks;
using Career.Cache.Attributes;
using Career.Cache.Helpers;
using Career.Utilities.Pagination;

namespace Definition.Application.Location.City
{
    public interface ICityService : IService
    {
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<PagedList<CityDto>> GetAsync(PaginationFilter paginationFilter);
        
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<PagedList<CityDto>> GetByCountryId(string countryId, PaginationFilter paginationFilter);
        
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<CityDto> GetByIdAsync(string id);
        
        [CacheInvalidate]
        Task<CityDto> CreateAsync(CityRequestModel requestModel);
        
        [CacheInvalidate]
        Task<CityDto> UpdateAsync(string id, CityRequestModel requestModel);
        
        [CacheInvalidate]
        Task DeleteAsync(string id);
    }
}