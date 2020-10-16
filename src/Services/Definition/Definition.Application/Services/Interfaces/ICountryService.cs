using System.Threading.Tasks;
using Career.Utilities.Pagination;
using Definition.Application.Dtos;
using Definition.Application.Models.RequestModels;

namespace Definition.Application.Services.Interfaces
{
    public interface ICountryService : IService
    {
        Task<PagedList<CountryDto>> GetAsync(PaginationFilter paginationFilter);
        Task<CountryDto> GetByIdAsync(string id);
        Task<CountryDto> CreateAsync(CountryRequestModel requestModel);
        Task<CountryDto> UpdateAsync(string id, CountryRequestModel requestModel);
        Task DeleteAsync(string id);
    }
}