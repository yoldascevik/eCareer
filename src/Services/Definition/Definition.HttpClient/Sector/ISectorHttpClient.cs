using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Http;
using Career.Utilities.Pagination;
using Definition.Contract.Dto;

namespace Definition.HttpClient.Sector
{
    public interface ISectorHttpClient: ICareerHttpClient
    {
        Task<ConsistentApiResponse<PagedList<SectorDto>>> GetAsync(PaginationFilter paginationFilter, string version);
        
        Task<ConsistentApiResponse<PagedList<JobPositionDto>>> GetJobPositionsOfSector(string sectorId, PaginationFilter paginationFilter, string version);
        
        Task<ConsistentApiResponse<SectorDto>> GetByIdAsync(string id, string version);
    }
}