using Career.Domain.DomainEvent.Dispatcher;
using Career.Mongo.Context;
using Career.Mongo.Repository;
using Job.Domain.JobAdvertAggregate;
using Job.Domain.JobAdvertAggregate.Repositories;

namespace Job.Infrastructure.Repositories
{
    public class JobAdvertRepository: MongoRepository<JobAdvert>, IJobAdvertRepository
    {
        public JobAdvertRepository(IMongoContext context, IDomainEventDispatcher domainEventDispatcher) 
            : base(context, domainEventDispatcher)
        {
        }
    }
}