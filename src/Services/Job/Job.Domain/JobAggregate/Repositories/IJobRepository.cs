using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job.Domain.TagAggregate;

namespace Job.Domain.JobAggregate.Repositories
{
    public interface IJobRepository
    {
        Task<Job> GetByIdAsync(Guid jobId);
        Task<Job> AddAsync(Job job);
        Task<Job> UpdateAsync(Guid jobId, Job job);
        Task<IEnumerable<Job>> GetByTag(Tag tag);
    }
}