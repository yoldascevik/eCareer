using System.Collections.Generic;
using System.Threading.Tasks;
using Job.Domain.TagAggregate;

namespace Job.Domain.JobAggregate.Services
{
    public interface IJobDomainService
    {
        Task<Job> UpdateTagsAsync(Job job, IEnumerable<string> tagNames);
        Task RemoveTagsFromJobsAsync(Tag tag);
        Task UpdateTagNameFromJobsAsync(Tag tag);
    }
}