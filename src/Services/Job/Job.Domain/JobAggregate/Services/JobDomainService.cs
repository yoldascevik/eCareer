using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Career.Exceptions;
using Job.Domain.JobAggregate.Repositories;
using Job.Domain.TagAggregate;

namespace Job.Domain.JobAggregate.Services
{
    public class JobDomainService: IJobDomainService
    {
        private readonly IJobRepository _jobRepository;
        
        public JobDomainService(IJobRepository jobRepository)
        {
            Check.NotNull(jobRepository, nameof(jobRepository));
            _jobRepository = jobRepository;
        }
        
        public async Task<Job> UpdateTags(Job job, IEnumerable<string> tagNames)
        {
            string[] existingJobTags = job.Tags.Select(x=> x.Name).ToArray();
            string[] addedTags = tagNames.Except(existingJobTags).ToArray();
            List<TagRef> deletedTags = job.Tags.Where(x => !tagNames.Contains(x.Name)).ToList();

            foreach (var deletedTag in deletedTags)
            {
                job.RemoveTag(deletedTag);
            }

            foreach (var addedTag in addedTags)
            {
                job.AddTag(Tag.Create(addedTag));
            }

            await _jobRepository.UpdateAsync(job.Id, job);
            return job;
        }
    }
}