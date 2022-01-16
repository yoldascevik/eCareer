using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;

namespace Definition.HttpClient.District;

public interface IDistrictHttpClient: ICareerHttpClient
{
    Task<ConsistentApiResponse<PagedList<DistrictDto>>> GetAsync(PaginationFilter paginationFilter);
        
    Task<ConsistentApiResponse<DistrictDto>> GetByIdAsync(string id);
}