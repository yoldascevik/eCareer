using System.Threading.Tasks;
using Career.Cache.Attributes;
using Career.Cache.Helpers;
using Career.Utilities.Pagination;
using Definition.Application.Dtos;
using Definition.Application.Models.RequestModels;

namespace Definition.Application.Services.Interfaces
{
    public interface ICityService : IService
    {
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<PagedList<CityDto>> GetAsync(PaginationFilter paginationFilter);
        
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<CityDto> GetByIdAsync(string id);
        
        Task<CityDto> CreateAsync(CityRequestModel requestModel);
        
        [CacheInvalidate(typeof(ICityService), "GetAsync")]
        [CacheInvalidate(typeof(ICityService), "GetByIdAsync")]
        Task<CityDto> UpdateAsync(string id, CityRequestModel requestModel);
        Task DeleteAsync(string id);
    }
}