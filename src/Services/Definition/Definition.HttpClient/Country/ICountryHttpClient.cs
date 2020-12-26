using System.Threading.Tasks;
using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;

namespace Definition.HttpClient.Country
{
    public interface ICountryHttpClient: ICareerHttpClient
    {
        Task<ConsistentApiResponse<PagedList<CountryDto>>> GetAsync(PaginationFilter paginationFilter, string version = null);
        
        Task<ConsistentApiResponse<CountryDto>> GetByIdAsync(string id, string version = null);
        
        Task<ConsistentApiResponse<CountryDto>> GetByCodeAsync(string code, string version = null);

        Task<ConsistentApiResponse<PagedList<CityDto>>> GetCitiesOfCountryAsync(string countryId, PaginationFilter paginationFilter, string version = null);
    }
}