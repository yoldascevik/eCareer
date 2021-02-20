using System;
using System.Threading.Tasks;
using Career.Repositories.Repository;

namespace Job.Domain.JobApplicationAggregate.Repositories
{
    public interface IJobApplicationRepository: IRepository<JobApplication>
    {
        Task<bool> IsJobApplicationExists(Guid userId, Guid jobAdvertId);
    }
}