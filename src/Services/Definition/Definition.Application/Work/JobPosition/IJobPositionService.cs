using System.Threading.Tasks;
using Career.Cache.Attributes;
using Career.Cache.Helpers;
using Career.Utilities.Pagination;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

namespace Definition.Application.Work.JobPosition
{
    public interface IJobPositionService : IService
    {
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<PagedList<JobPositionDto>> GetAsync(PaginationFilter paginationFilter);
        
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<PagedList<JobPositionDto>> GetBySectorId(string sectorId, PaginationFilter paginationFilter);
        
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<JobPositionDto> GetByIdAsync(string id);

        [CacheInvalidate]
        Task<JobPositionDto> CreateAsync(JobPositionRequestModel requestModel);
        
        [CacheInvalidate]
        Task<JobPositionDto> UpdateAsync(string id, JobPositionRequestModel requestModel);
        
        [CacheInvalidate]
        Task DeleteAsync(string id);
    }
}