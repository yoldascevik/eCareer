using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;

namespace Definition.HttpClient.JobPosition;

public interface IJobPositionHttpClient: ICareerHttpClient
{
    Task<ConsistentApiResponse<PagedList<JobPositionDto>>> GetAsync(PaginationFilter paginationFilter);
        
    Task<ConsistentApiResponse<JobPositionDto>> GetByIdAsync(string id);
}