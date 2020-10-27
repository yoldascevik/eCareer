using System.Threading.Tasks;
using Career.Cache;
using Career.Utilities.Pagination;
using Definition.Application.Dtos;
using Definition.Application.Models.RequestModels;

namespace Definition.Application.Services.Interfaces
{
    public interface ICityService : IService
    {
        [Cache]
        Task<PagedList<CityDto>> GetAsync(PaginationFilter paginationFilter);
        
        [Cache]
        Task<CityDto> GetByIdAsync(string id);
        Task<CityDto> CreateAsync(CityRequestModel requestModel);
        Task<CityDto> UpdateAsync(string id, CityRequestModel requestModel);
        Task DeleteAsync(string id);
    }
}