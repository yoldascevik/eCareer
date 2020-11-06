using System.Threading.Tasks;
using Career.Cache.Attributes;
using Career.Cache.Helpers;
using Career.Utilities.Pagination;

namespace Definition.Application.Location.Country
{
    public interface ICountryService : IService
    {
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<PagedList<CountryDto>> GetAsync(PaginationFilter paginationFilter);
        
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<CountryDto> GetByIdAsync(string id);
        
        [CacheInvalidate]
        Task<CountryDto> CreateAsync(CountryRequestModel requestModel);
        
        [CacheInvalidate]
        Task<CountryDto> UpdateAsync(string id, CountryRequestModel requestModel);
        
        [CacheInvalidate]
        Task DeleteAsync(string id);
    }
}