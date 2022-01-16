using ARConsistency.Abstractions;
using Career.Data.Pagination;
using Career.Http.HttpClient;
using Definition.Contract.Dto;

namespace Definition.HttpClient.Country;

public interface ICountryHttpClient: ICareerHttpClient
{
    Task<ConsistentApiResponse<PagedList<CountryDto>>> GetAsync(PaginationFilter paginationFilter);
        
    Task<ConsistentApiResponse<CountryDto>> GetByIdAsync(string id);
        
    Task<ConsistentApiResponse<CountryDto>> GetByCodeAsync(string code);

    Task<ConsistentApiResponse<PagedList<CityDto>>> GetCitiesOfCountryAsync(string countryId, PaginationFilter paginationFilter);
}