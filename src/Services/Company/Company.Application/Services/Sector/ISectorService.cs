using System.Threading.Tasks;

namespace Company.Application.Services.Sector
{
    public interface ISectorService
    {
        Task<bool> IsSectorExistsAsync(string sectorId);
    }
}