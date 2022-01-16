using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;

namespace Definition.HttpClient.Sector;

public interface ISectorHttpClient: ICareerHttpClient
{
    Task<ConsistentApiResponse<PagedList<SectorDto>>> GetAsync(PaginationFilter paginationFilter);
        
    Task<ConsistentApiResponse<PagedList<JobPositionDto>>> GetJobPositionsOfSector(string sectorId, PaginationFilter paginationFilter);
        
    Task<ConsistentApiResponse<SectorDto>> GetByIdAsync(string id);
}