using System.Threading.Tasks;

namespace Company.Application.Services.Abstractions
{
    public interface ISectorService
    {
        Task<bool> IsSectorExistsAsync(string sectorId);
    }
}