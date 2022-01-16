using Career.Cache.Attributes;
using Career.Cache.Helpers;
using Career.Data.Pagination;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

namespace Definition.Application.Education.ScholarshipType;

public interface IScholarshipTypeService : IService
{
    [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
    Task<PagedList<ScholarshipTypeDto>> GetAsync(PaginationFilter paginationFilter);
        
    [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
    Task<ScholarshipTypeDto> GetByIdAsync(string id);
        
    [CacheInvalidate]
    Task<ScholarshipTypeDto> CreateAsync(ScholarshipTypeRequestModel requestModel);
        
    [CacheInvalidate]
    Task<ScholarshipTypeDto> UpdateAsync(string id, ScholarshipTypeRequestModel requestModel);
        
    [CacheInvalidate]
    Task DeleteAsync(string id);
}