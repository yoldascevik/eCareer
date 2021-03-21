using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job.Domain.JobAdvertAggregate.Services
{
    public interface IJobAdvertDomainService
    {
        Task<JobAdvert> UpdateTags(JobAdvert jobAdvert, IEnumerable<string> tagNames);
    }
}