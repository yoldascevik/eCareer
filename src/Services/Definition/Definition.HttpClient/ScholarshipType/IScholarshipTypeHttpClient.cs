using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;

namespace Definition.HttpClient.ScholarshipType;

public interface IScholarshipTypeHttpClient: ICareerHttpClient
{
    Task<ConsistentApiResponse<PagedList<ScholarshipTypeDto>>> GetAsync(PaginationFilter paginationFilter);
        
    Task<ConsistentApiResponse<ScholarshipTypeDto>> GetByIdAsync(string id);
}