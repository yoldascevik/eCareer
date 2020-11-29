using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Definition.Contract.Dto;
using Definition.HttpClient.City;
using Definition.HttpClient.Country;
using Definition.HttpClient.District;

namespace Company.Application.Services.Location
{
    public class LocationService: ILocationService
    {
        private readonly ICityHttpClient _cityHttpClient;
        private readonly ICountryHttpClient _countryHttpClient;
        private readonly IDistrictHttpClient _districtHttpClient;

        public LocationService(ICityHttpClient cityHttpClient, ICountryHttpClient countryHttpClient, IDistrictHttpClient districtHttpClient)
        {
            _cityHttpClient = cityHttpClient;
            _countryHttpClient = countryHttpClient;
            _districtHttpClient = districtHttpClient;
        }

        public async Task<bool> IsCountryExistsAsync(string countryId)
        {
            ConsistentApiResponse<CountryDto> response = await _countryHttpClient.GetByIdAsync(countryId);
            return response?.Payload != null;
        }

        public async Task<bool> IsCityExistsInCountryAsync(string cityId, string countryId = null)
        {
            ConsistentApiResponse<CityDto> response = await _cityHttpClient.GetByIdAsync(cityId);
            if (response?.Payload == null)
                return false;

            if (countryId != null)
                return response.Payload.CountryId == countryId;

            return true;
        }

        public async Task<bool> IsDistrictExistsInCityAsync(string districtId, string cityId = null)
        {
            ConsistentApiResponse<DistrictDto> response = await _districtHttpClient.GetByIdAsync(districtId);
            if (response?.Payload == null)
                return false;

            if (cityId != null)
                return response.Payload.CityId == cityId;

            return true;
        }
    }
}