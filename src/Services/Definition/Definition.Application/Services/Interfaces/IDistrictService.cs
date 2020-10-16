using System.Threading.Tasks;
using Career.Utilities.Pagination;
using Definition.Application.Dtos;
using Definition.Application.Models.RequestModels;

namespace Definition.Application.Services.Interfaces
{
    public interface IDistrictService : IService
    {
        Task<PagedList<DistrictDto>> GetAsync(PaginationFilter paginationFilter);
        Task<DistrictDto> GetByIdAsync(string id);
        Task<DistrictDto> CreateAsync(DistrictRequestModel requestModel);
        Task<DistrictDto> UpdateAsync(string id, DistrictRequestModel requestModel);
        Task DeleteAsync(string id);
    }
}