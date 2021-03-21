using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job.Domain.TagAggregate;

namespace Job.Domain.JobAdvertAggregate.Repositories
{
    public interface IJobAdvertRepository
    {
        Task<JobAdvert> GetByIdAsync(Guid jobAdvertId);
        Task<JobAdvert> AddAsync(JobAdvert jobAdvert);
        Task<JobAdvert> UpdateAsync(Guid jobAdvertId, JobAdvert jobAdvert);
        Task<IEnumerable<JobAdvert>> GetByTag(Tag tag);
    }
}