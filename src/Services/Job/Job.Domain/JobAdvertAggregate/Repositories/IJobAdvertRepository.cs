using System.Threading.Tasks;
using Career.Domain.DomainEvent.Dispatcher;
using Career.Repositories.Repository;

namespace Job.Domain.JobAdvertAggregate.Repositories
{
    //TODO:Test
    [DispacthDomainEvent]
    public interface IJobAdvertRepository: IRepository<JobAdvert>
    {
        /// <summary>
        /// TODO: will be remove
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [DispacthDomainEvent]
        Task<JobAdvert> UpdateAsync2(object key, JobAdvert item);
    }
}