using System.Threading.Tasks;
using Career.Cache;
using Career.Utilities.Pagination;
using Definition.Application.Dtos;
using Definition.Application.Models.RequestModels;

namespace Definition.Application.Services.Interfaces
{
    public interface ICountryService : IService
    {
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<PagedList<CountryDto>> GetAsync(PaginationFilter paginationFilter);
        
        [Cache(TTL = 30 * TTLMultiplier.Day, SlidingExpiration = false)]
        Task<CountryDto> GetByIdAsync(string id);
        
        Task<CountryDto> CreateAsync(CountryRequestModel requestModel);
        Task<CountryDto> UpdateAsync(string id, CountryRequestModel requestModel);
        Task DeleteAsync(string id);
    }
}