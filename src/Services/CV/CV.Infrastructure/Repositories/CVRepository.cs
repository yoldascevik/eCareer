using Career.EventHub;
using Career.Mongo.Context;
using Career.Mongo.Repository;
using CurriculumVitae.Core.Entities;
using CurriculumVitae.Core.Repositories;
using CurriculumVitae.Data.Entities;

namespace CurriculumVitae.Infrastructure.Repositories
{
    public class CVRepository: MongoRepository<CV>, ICVRepository
    {
        public CVRepository(IMongoContext context, IEventDispatcher domainEventDispatcher) : base(context, domainEventDispatcher)
        {
        }
    }
}