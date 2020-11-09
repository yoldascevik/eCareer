using System.Threading.Tasks;
using Career.Cache.Attributes;
using Career.Cache.Helpers;
using Career.Utilities.Pagination;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

namespace Definition.Application.Language
{
    public interface ILanguageService : IService
    {
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<PagedList<LanguageDto>> GetAsync(PaginationFilter paginationFilter);
        
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<LanguageDto> GetByIdAsync(string id);
        
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<LanguageDto> GetByCultureAsync(string culture);
        
        [CacheInvalidate]
        Task<LanguageDto> CreateAsync(LanguageRequestModel requestModel);
        
        [CacheInvalidate]
        Task<LanguageDto> UpdateAsync(string id, LanguageRequestModel requestModel);
        
        [CacheInvalidate]
        Task DeleteAsync(string id);
    }
}