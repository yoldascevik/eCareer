using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Definition.Contract.Dto;
using Definition.HttpClient.Sector;

namespace Company.Application.Services.Sector
{
    public class SectorService: ISectorService
    {
        private readonly ISectorHttpClient _sectorHttpClient;

        public SectorService(ISectorHttpClient sectorHttpClient)
        {
            _sectorHttpClient = sectorHttpClient;
        }

        public async Task<bool> IsSectorExistsAsync(string sectorId)
        {
            ConsistentApiResponse<SectorDto> response = await _sectorHttpClient.GetByIdAsync(sectorId);
            return response?.Payload != null;
        }
    }
}