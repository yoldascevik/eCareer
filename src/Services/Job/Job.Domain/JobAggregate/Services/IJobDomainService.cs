using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job.Domain.JobAggregate.Services
{
    public interface IJobDomainService
    {
        Task<Job> UpdateTags(Job job, IEnumerable<string> tagNames);
    }
}