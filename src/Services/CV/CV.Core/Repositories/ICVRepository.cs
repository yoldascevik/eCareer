using Career.Mongo.Repository.Contracts;
using CurriculumVitae.Core.Entities;

namespace CurriculumVitae.Core.Repositories
{
    public interface ICVRepository: IMongoRepository<CV>
    {
        
    }
}