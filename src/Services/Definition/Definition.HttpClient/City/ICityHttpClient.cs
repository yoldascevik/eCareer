using Career.Http;

namespace Definition.HttpClient.City
{
    public interface ICityHttpClient : ICareerHttpClient
    {
        Task<PagedList<CityDto>> GetAsync(PaginationFilter paginationFilter);
        
        Task<CityDto> GetByIdAsync(string id);
        
        Task<PagedList<CityDto>> GetByCountryId(string countryId, PaginationFilter paginationFilter);
    }
}