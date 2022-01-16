using Career.Cache.Attributes;
using Career.Cache.Helpers;
using Career.Data.Pagination;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

namespace Definition.Application.Work.Sector;

public interface ISectorService : IService
{
    [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
    Task<PagedList<SectorDto>> GetAsync(PaginationFilter paginationFilter);
        
    [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
    Task<SectorDto> GetByIdAsync(string id);
        
    [CacheInvalidate]
    Task<SectorDto> CreateAsync(SectorRequestModel requestModel);
        
    [CacheInvalidate]
    Task<SectorDto> UpdateAsync(string id, SectorRequestModel requestModel);
        
    [CacheInvalidate]
    Task DeleteAsync(string id);
}