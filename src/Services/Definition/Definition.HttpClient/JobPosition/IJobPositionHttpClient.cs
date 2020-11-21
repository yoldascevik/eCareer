using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Http;
using Career.Utilities.Pagination;
using Definition.Contract.Dto;

namespace Definition.HttpClient.JobPosition
{
    public interface IJobPositionHttpClient: ICareerHttpClient
    {
        Task<ConsistentApiResponse<PagedList<JobPositionDto>>> GetAsync(PaginationFilter paginationFilter, string version);
        
        Task<ConsistentApiResponse<JobPositionDto>> GetByIdAsync(string id, string version);
    }
}