using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;

namespace Definition.HttpClient.WorkType
{
    public interface IWorkTypeHttpClient: ICareerHttpClient
    {
        Task<ConsistentApiResponse<PagedList<WorkTypeDto>>> GetAsync(PaginationFilter paginationFilter);
        
        Task<ConsistentApiResponse<WorkTypeDto>> GetByIdAsync(string id);
    }
}