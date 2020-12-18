using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http;
using Definition.Contract.Dto;

namespace Definition.HttpClient.WorkType
{
    public interface IWorkTypeHttpClient: ICareerHttpClient
    {
        Task<ConsistentApiResponse<PagedList<WorkTypeDto>>> GetAsync(PaginationFilter paginationFilter, string version = null);
        
        Task<ConsistentApiResponse<WorkTypeDto>> GetByIdAsync(string id, string version = null);
    }
}