using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;

namespace Definition.HttpClient.Sector
{
    public interface ISectorHttpClient: ICareerHttpClient
    {
        Task<ConsistentApiResponse<PagedList<SectorDto>>> GetAsync(PaginationFilter paginationFilter, string version = null);
        
        Task<ConsistentApiResponse<PagedList<JobPositionDto>>> GetJobPositionsOfSector(string sectorId, PaginationFilter paginationFilter, string version = null);
        
        Task<ConsistentApiResponse<SectorDto>> GetByIdAsync(string id, string version = null);
    }
}