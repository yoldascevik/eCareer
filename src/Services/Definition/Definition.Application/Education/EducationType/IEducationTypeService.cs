using System.Threading.Tasks;
using Career.Cache.Attributes;
using Career.Cache.Helpers;
using Career.Utilities.Pagination;
using Definition.Contract.RequestModel;

namespace Definition.Application.Education.EducationType
{
    public interface IEducationTypeService : IService
    {
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<PagedList<EducationTypeDto>> GetAsync(PaginationFilter paginationFilter);
        
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<EducationTypeDto> GetByIdAsync(string id);
        
        [CacheInvalidate]
        Task<EducationTypeDto> CreateAsync(EducationTypeRequestModel requestModel);
        
        [CacheInvalidate]
        Task<EducationTypeDto> UpdateAsync(string id, EducationTypeRequestModel requestModel);
        
        [CacheInvalidate]
        Task DeleteAsync(string id);
    }
}