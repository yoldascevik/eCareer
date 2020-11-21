using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Http;
using Career.Utilities.Pagination;
using Definition.Contract.Dto;

namespace Definition.HttpClient.Language
{
    public interface ILanguageHttpClient: ICareerHttpClient
    {
        Task<ConsistentApiResponse<PagedList<LanguageDto>>> GetAsync(PaginationFilter paginationFilter, string version);
        
        Task<ConsistentApiResponse<LanguageDto>> GetByIdAsync(string id, string version);
        Task<ConsistentApiResponse<LanguageDto>> GetByCultureAsync(string culture, string version);
    }
}