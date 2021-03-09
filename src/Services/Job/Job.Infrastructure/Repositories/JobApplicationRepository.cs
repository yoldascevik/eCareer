using System;
using System.Threading.Tasks;
using Career.Domain.DomainEvent.Dispatcher;
using Career.Mongo.Context;
using Career.Mongo.Repository;
using Job.Domain.JobApplicationAggregate;
using Job.Domain.JobApplicationAggregate.Repositories;

namespace Job.Infrastructure.Repositories
{
    public class JobApplicationRepository: MongoRepository<JobApplication>, IJobApplicationRepository
    {
        public JobApplicationRepository(IMongoContext context, IDomainEventDispatcher domainEventDispatcher) 
            : base(context, domainEventDispatcher)
        {
        }

        public async Task<bool> IsJobApplicationExists(Guid userId, Guid jobAdvertId)
        {
            return await AnyAsync(x => x.JobAdvertId == jobAdvertId && x.UserId == userId);
        }
    }
}