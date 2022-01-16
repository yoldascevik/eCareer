using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;

namespace Definition.HttpClient.EducationType;

public interface IEducationTypeHttpClient: ICareerHttpClient
{
    Task<ConsistentApiResponse<PagedList<EducationTypeDto>>> GetAsync(PaginationFilter paginationFilter);
        
    Task<ConsistentApiResponse<EducationTypeDto>> GetByIdAsync(string id);
}