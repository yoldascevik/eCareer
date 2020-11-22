using System.Threading;
using System.Threading.Tasks;
using ARConsistency.Abstractions;
using AutoMapper;
using Career.Exceptions.Exceptions;
using Company.Application.Dtos;
using Company.Domain.Repository;
using Definition.Contract.Dto;
using Definition.HttpClient.City;
using Definition.HttpClient.Country;
using Definition.HttpClient.District;
using Definition.HttpClient.Sector;
using MediatR;

namespace Company.Application.Company.CreateCompany
{
    public class CreateCompanyHandler: IRequestHandler<CreateCompanyCommand, CompanyDto>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICityHttpClient _cityHttpClient;
        private readonly ISectorHttpClient _sectorHttpClient;
        private readonly ICountryHttpClient _countryHttpClient;
        private readonly IDistrictHttpClient _districtHttpClient;

        public CreateCompanyHandler(
            IMapper mapper, 
            ICompanyRepository companyRepository, 
            ICountryHttpClient countryHttpClient, 
            ICityHttpClient cityHttpClient, 
            IDistrictHttpClient districtHttpClient, 
            ISectorHttpClient sectorHttpClient)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _countryHttpClient = countryHttpClient;
            _cityHttpClient = cityHttpClient;
            _districtHttpClient = districtHttpClient;
            _sectorHttpClient = sectorHttpClient;
        }
        
        public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            ConsistentApiResponse<CountryDto> country = await _countryHttpClient.GetByIdAsync(request.CountryId);
            if (country?.Payload == null)
                throw new NotFoundException($"Country not found for Id:{request.CountryId}");
            
            ConsistentApiResponse<CityDto> city = await _cityHttpClient.GetByIdAsync(request.CityId);
            if (city?.Payload == null)
                throw new NotFoundException($"City not found for Id:{request.CityId}");
            else if(city.Payload.CountryId != country.Payload.Id)
                throw new NotFoundException($"City not found for Id:{request.CityId} in country {country.Payload.Id}");

            if (!string.IsNullOrEmpty(request.DistrictId))
            {
                ConsistentApiResponse<DistrictDto> district = await _districtHttpClient.GetByIdAsync(request.DistrictId);
                if (district?.Payload == null)
                    throw new NotFoundException($"District not found for Id:{request.DistrictId}");
                else if(district.Payload.CityId != city.Payload.Id)
                    throw new NotFoundException($"District not found for Id:{request.DistrictId} in city {city.Payload.Id}");
            }
            
            ConsistentApiResponse<SectorDto> sector = await _sectorHttpClient.GetByIdAsync(request.SectorId);
            if (sector?.Payload == null)
                throw new NotFoundException($"Sector not found for Id:{request.SectorId}");

            if (_companyRepository.IsTaxNumberExists(request.TaxNumber, request.CountryId))
                throw new ItemAlreadyExistsException($"TaxNumber already registered in system: {request.TaxNumber}");
            
            var createdCompany = _mapper.Map<Domain.Company>(request);
            return _mapper.Map<CompanyDto>(await _companyRepository.AddAsync(createdCompany));
        }
    }
}