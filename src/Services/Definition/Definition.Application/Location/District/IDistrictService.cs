using System.Threading.Tasks;
using Career.Cache.Attributes;
using Career.Cache.Helpers;
using Career.Utilities.Pagination;
using Definition.Contract.RequestModel;

namespace Definition.Application.Location.District
{
    public interface IDistrictService : IService
    {
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<PagedList<DistrictDto>> GetAsync(PaginationFilter paginationFilter);
        
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<PagedList<DistrictDto>> GetByCityId(string cityId, PaginationFilter paginationFilter);
        
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<DistrictDto> GetByIdAsync(string id);
        
        [CacheInvalidate]
        Task<DistrictDto> CreateAsync(DistrictRequestModel requestModel);

        [CacheInvalidate]
        Task<DistrictDto> UpdateAsync(string id, DistrictRequestModel requestModel);
        
        [CacheInvalidate]
        Task DeleteAsync(string id);
    }
}