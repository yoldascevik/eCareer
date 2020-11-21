using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Http;
using Career.Utilities.Pagination;
using Definition.Contract.Dto;

namespace Definition.HttpClient.EducationLevel
{
    public interface IEducationLevelHttpClient: ICareerHttpClient
    {
        Task<ConsistentApiResponse<PagedList<EducationLevelDto>>> GetAsync(PaginationFilter paginationFilter, string version);
        
        Task<ConsistentApiResponse<EducationLevelDto>> GetByIdAsync(string id, string version);
    }
}