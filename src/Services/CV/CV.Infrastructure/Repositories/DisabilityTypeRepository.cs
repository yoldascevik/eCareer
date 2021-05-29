using System.Threading.Tasks;
using Career.EventHub;
using Career.Mongo.Context;
using Career.Mongo.Repository;
using CurriculumVitae.Core.Entities;
using CurriculumVitae.Core.Repositories;

namespace CurriculumVitae.Infrastructure.Repositories
{
    public class DisabilityTypeRepository : MongoRepository<DisabilityType>, IDisabilityTypeRepository
    {
        public DisabilityTypeRepository(IMongoContext context, IEventDispatcher domainEventDispatcher)
            : base(context, domainEventDispatcher)
        {
        }

        public async Task<bool> ExistsByIdAsync(string id)
            => await AnyAsync(x => x.Id == id);
    }
}