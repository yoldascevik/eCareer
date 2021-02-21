using System.Threading.Tasks;
using Career.Mongo.Context;
using Career.Mongo.Repository;
using Job.Domain.JobAdvertAggregate;
using Job.Domain.JobAdvertAggregate.Repositories;

namespace Job.Infrastructure.Repositories
{
    public class JobAdvertRepository: MongoRepository<JobAdvert>, IJobAdvertRepository
    {
        public JobAdvertRepository(IMongoContext context) : base(context)
        {
        }

        //TODO : will be remove
        public async Task<JobAdvert> UpdateAsync2(object key, JobAdvert item)
        {
            return await Task.FromResult(item);
        }
    }
}