using Career.Repositories.Repository;
using CurriculumVitae.Core.Entities;

namespace CurriculumVitae.Core.Repositories;

public interface ISocialProfileTypeRepository : IRepository<SocialProfileType>
{
    Task<bool> ExistsByNameAsync(string name);
    Task<bool> ExistsByNameAsync(string name, string excludeId);
}