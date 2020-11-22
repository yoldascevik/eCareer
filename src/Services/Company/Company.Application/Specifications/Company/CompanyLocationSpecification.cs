using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Exceptions.Exceptions;
using Definition.Contract.Dto;
using Definition.HttpClient.City;
using Definition.HttpClient.Country;
using Definition.HttpClient.District;

namespace Company.Application.Specifications.Company
{
    public class CompanyLocationSpecification: ICompanyLocationSpecification
    {
        private readonly ICityHttpClient _cityHttpClient;
        private readonly ICountryHttpClient _countryHttpClient;
        private readonly IDistrictHttpClient _districtHttpClient;
        
        public CompanyLocationSpecification(
            ICityHttpClient cityHttpClient, 
            ICountryHttpClient countryHttpClient, 
            IDistrictHttpClient districtHttpClient)
        {
            _cityHttpClient = cityHttpClient;
            _countryHttpClient = countryHttpClient;
            _districtHttpClient = districtHttpClient;
        }
        
        public async Task<bool> IsSatisfiedByAsync(Domain.Company company)
        {
            ConsistentApiResponse<CountryDto> country = await _countryHttpClient.GetByIdAsync(company.CountryId);
            if (country?.Payload == null)
                throw new NotFoundException($"Country not found for Id:{company.CountryId}");
            
            ConsistentApiResponse<CityDto> city = await _cityHttpClient.GetByIdAsync(company.CityId);
            if (city?.Payload == null)
                throw new NotFoundException($"City not found for Id:{company.CityId}");
            else if(city.Payload.CountryId != country.Payload.Id)
                throw new NotFoundException($"City not found for Id:{company.CityId} in country {country.Payload.Id}");

            if (!string.IsNullOrEmpty(company.DistrictId))
            {
                ConsistentApiResponse<DistrictDto> district = await _districtHttpClient.GetByIdAsync(company.DistrictId);
                if (district?.Payload == null)
                    throw new NotFoundException($"District not found for Id:{company.DistrictId}");
                else if(district.Payload.CityId != city.Payload.Id)
                    throw new NotFoundException($"District not found for Id:{company.DistrictId} in city {city.Payload.Id}");
            }

            return true;
        }
    }
}