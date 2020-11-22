using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Http;
using Career.Utilities.Pagination;
using Definition.Contract.Dto;

namespace Definition.HttpClient.EducationType
{
    public interface IEducationTypeHttpClient: ICareerHttpClient
    {
        Task<ConsistentApiResponse<PagedList<EducationTypeDto>>> GetAsync(PaginationFilter paginationFilter, string version = null);
        
        Task<ConsistentApiResponse<EducationTypeDto>> GetByIdAsync(string id, string version = null);
    }
}