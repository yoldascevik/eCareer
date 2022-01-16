using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Job.Domain.TagAggregate;

namespace Job.Domain.JobAggregate.Repositories;

public interface IJobRepository
{
    IQueryable<Job> Get();
    IQueryable<Job> Get(Expression<Func<Job, bool>> condition);
    IQueryable<Job> GetPublishedJobs();
    Task<Job> GetByIdAsync(Guid jobId);
    Task<IEnumerable<Job>> GetByTagAsync(Tag tag);
    Task<bool> IsCandidateExistsAsync(Guid jobId, Guid userId);
    Task<Job> AddAsync(Job job);
    Task<Job> UpdateAsync(Guid jobId, Job job);
    Task UpdateCompanyNamesInJob(Guid companyId, string companyName);
}