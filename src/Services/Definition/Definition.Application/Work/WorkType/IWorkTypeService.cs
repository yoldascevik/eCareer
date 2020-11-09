using System.Threading.Tasks;
using Career.Cache.Attributes;
using Career.Cache.Helpers;
using Career.Utilities.Pagination;
using Definition.Contract.RequestModel;

namespace Definition.Application.Work.WorkType
{
    public interface IWorkTypeService : IService
    {
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<PagedList<WorkTypeDto>> GetAsync(PaginationFilter paginationFilter);
        
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<WorkTypeDto> GetByIdAsync(string id);
        
        [CacheInvalidate]
        Task<WorkTypeDto> CreateAsync(WorkTypeRequestModel requestModel);
        
        [CacheInvalidate]
        Task<WorkTypeDto> UpdateAsync(string id, WorkTypeRequestModel requestModel);
        
        [CacheInvalidate]
        Task DeleteAsync(string id);
    }
}