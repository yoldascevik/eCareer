using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;

namespace Definition.HttpClient.EducationLevel;

public interface IEducationLevelHttpClient: ICareerHttpClient
{
    Task<ConsistentApiResponse<PagedList<EducationLevelDto>>> GetAsync(PaginationFilter paginationFilter);
        
    Task<ConsistentApiResponse<EducationLevelDto>> GetByIdAsync(string id);
}