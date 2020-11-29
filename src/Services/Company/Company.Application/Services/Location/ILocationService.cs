using System.Threading.Tasks;

namespace Company.Application.Services.Location
{
    public interface ILocationService
    {
        Task<bool> IsCountryExistsAsync(string countryId);
        Task<bool> IsCityExistsInCountryAsync(string cityId, string countryId);
        Task<bool> IsDistrictExistsInCityAsync(string districtId, string cityId);
    }
}