using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http;
using Definition.Contract.Dto;

namespace Definition.HttpClient.ScholarshipType
{
    public interface IScholarshipTypeHttpClient: ICareerHttpClient
    {
        Task<ConsistentApiResponse<PagedList<ScholarshipTypeDto>>> GetAsync(PaginationFilter paginationFilter, string version = null);
        
        Task<ConsistentApiResponse<ScholarshipTypeDto>> GetByIdAsync(string id, string version = null);
    }
}