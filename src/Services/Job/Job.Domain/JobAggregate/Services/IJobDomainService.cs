using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job.Domain.JobAggregate.Services
{
    public interface IJobDomainService
    {
        Task<Job> UpdateTagsAsync(Job job, IEnumerable<string> tagNames);
    }
}