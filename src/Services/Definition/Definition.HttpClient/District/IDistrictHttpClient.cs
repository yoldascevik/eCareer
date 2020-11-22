using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Http;
using Career.Utilities.Pagination;
using Definition.Contract.Dto;

namespace Definition.HttpClient.District
{
    public interface IDistrictHttpClient: ICareerHttpClient
    {
        Task<ConsistentApiResponse<PagedList<DistrictDto>>> GetAsync(PaginationFilter paginationFilter, string version = null);
        
        Task<ConsistentApiResponse<DistrictDto>> GetByIdAsync(string id, string version = null);
    }
}