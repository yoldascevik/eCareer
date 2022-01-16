using System.Threading.Tasks;
using Career.Repositories.Repository;
using CurriculumVitae.Core.Entities;

namespace CurriculumVitae.Core.Repositories;

public interface ICVRepository : IRepository<CV>
{
    Task UpdateAllDisabilityTypeNamesInCV(DisabilityType disabilityType);
    Task UpdateAllSocialProfileTypesInCV(SocialProfileType socialProfileType);
}