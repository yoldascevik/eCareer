using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;
using Microsoft.AspNetCore.Http;

namespace Definition.HttpClient.Sector
{
    public class SectorHttpClient: CareerHttpClient, ISectorHttpClient
    {
        private readonly ApiEndpointOptions _apiEndpointOptions;
        
        public SectorHttpClient(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContext, ApiEndpointOptions apiEndpointOptions) 
            : base(httpClient, httpContext)
        {
            _apiEndpointOptions = apiEndpointOptions;
        }

        // api/v{version}/work/sectors
        public async Task<ConsistentApiResponse<PagedList<SectorDto>>> GetAsync(PaginationFilter paginationFilter, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<SectorDto>>>(CreateUrl(null, version), paginationFilter);
        }

        // api/v{version}/work/sectors/{id}/positions
        public async Task<ConsistentApiResponse<PagedList<JobPositionDto>>> GetJobPositionsOfSector(string sectorId, PaginationFilter paginationFilter, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<PagedList<JobPositionDto>>>(CreateUrl($"/{sectorId}/positions", version), paginationFilter);
        }

        // api/v{version}/work/sectors/{id}
        public async Task<ConsistentApiResponse<SectorDto>> GetByIdAsync(string id, string version = null)
        {
            return await GetAsync<ConsistentApiResponse<SectorDto>>(CreateUrl(null, version), id);
        }
        
        private string CreateUrl(string requestPath, string version)
        {
            if (string.IsNullOrEmpty(version))
                version = _apiEndpointOptions.DefaultVersion;

            return $"{_apiEndpointOptions.ApiUrl}/api/v{version}/work/sectors{requestPath ?? string.Empty}";
        }
    }
}