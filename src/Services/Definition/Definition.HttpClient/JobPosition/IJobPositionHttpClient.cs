using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http;
using Definition.Contract.Dto;

namespace Definition.HttpClient.JobPosition
{
    public interface IJobPositionHttpClient: ICareerHttpClient
    {
        Task<ConsistentApiResponse<PagedList<JobPositionDto>>> GetAsync(PaginationFilter paginationFilter, string version = null);
        
        Task<ConsistentApiResponse<JobPositionDto>> GetByIdAsync(string id, string version = null);
    }
}