using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Utilities.Pagination;
using Definition.Contract.Dto;

namespace Definition.HttpClient.Country
{
    public interface ICountryHttpClient
    {
        Task<ConsistentApiResponse<PagedList<CountryDto>>> GetAsync(PaginationFilter paginationFilter);
        
        Task<ConsistentApiResponse<CountryDto>> GetByIdAsync(string id);
        
        Task<ConsistentApiResponse<CountryDto>> GetByCodeAsync(string code);

        Task<ConsistentApiResponse<PagedList<CityDto>>> GetCitiesByCountryAsync(string countryId, PaginationFilter paginationFilter);
    }
}