using System.Threading.Tasks;
using Career.Repositories.Repository;
using CurriculumVitae.Core.Entities;

namespace CurriculumVitae.Core.Repositories
{
    public interface IDisabilityTypeRepository : IRepository<DisabilityType>
    {
        Task<bool> ExistsByIdAsync(string id);
    }
}