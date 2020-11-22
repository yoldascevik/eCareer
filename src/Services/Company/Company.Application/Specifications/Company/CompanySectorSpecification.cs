using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Exceptions.Exceptions;
using Definition.Contract.Dto;
using Definition.HttpClient.Sector;

namespace Company.Application.Specifications.Company
{
    public class CompanySectorSpecification: ICompanySectorSpecification
    {
        private readonly ISectorHttpClient _sectorHttpClient;

        public CompanySectorSpecification(ISectorHttpClient sectorHttpClient)
        {
            _sectorHttpClient = sectorHttpClient;
        }

        public async Task<bool> IsSatisfiedByAsync(Domain.Company company)
        {
            ConsistentApiResponse<SectorDto> sector = await _sectorHttpClient.GetByIdAsync(company.SectorId);
            if (sector?.Payload == null)
                throw new NotFoundException($"Sector not found for Id:{company.SectorId}");

            return true;
        }
    }
}