using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;

namespace Definition.HttpClient.Language;

public interface ILanguageHttpClient: ICareerHttpClient
{
    Task<ConsistentApiResponse<PagedList<LanguageDto>>> GetAsync(PaginationFilter paginationFilter);
    Task<ConsistentApiResponse<LanguageDto>> GetByIdAsync(string id);
    Task<ConsistentApiResponse<LanguageDto>> GetByCultureAsync(string culture);
}