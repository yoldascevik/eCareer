using System.Threading.Tasks;
using Career.Cache.Attributes;
using Career.Cache.Helpers;
using Career.Utilities.Pagination;

namespace Definition.Application.Education.EducationLevel
{
    public interface IEducationLevelService : IService
    {
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<PagedList<EducationLevelDto>> GetAsync(PaginationFilter paginationFilter);
        
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<EducationLevelDto> GetByIdAsync(string id);
        
        [CacheInvalidate]
        Task<EducationLevelDto> CreateAsync(EducationLevelRequestModel requestModel);
        
        [CacheInvalidate]
        Task<EducationLevelDto> UpdateAsync(string id, EducationLevelRequestModel requestModel);
        
        [CacheInvalidate]
        Task DeleteAsync(string id);
    }
}