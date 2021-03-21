using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Career.Exceptions;
using Job.Domain.JobAdvertAggregate.Repositories;
using Job.Domain.TagAggregate;

namespace Job.Domain.JobAdvertAggregate.Services
{
    public class JobAdvertDomainService: IJobAdvertDomainService
    {
        private readonly IJobAdvertRepository _jobAdvertRepository;
        
        public JobAdvertDomainService(IJobAdvertRepository jobAdvertRepository)
        {
            Check.NotNull(jobAdvertRepository, nameof(jobAdvertRepository));
            _jobAdvertRepository = jobAdvertRepository;
        }
        
        public async Task<JobAdvert> UpdateTags(JobAdvert jobAdvert, IEnumerable<string> tagNames)
        {
            string[] existingJobAdvertTags = jobAdvert.Tags.Select(x=> x.Name).ToArray();
            string[] addedTags = tagNames.Except(existingJobAdvertTags).ToArray();
            List<TagRef> deletedTags = jobAdvert.Tags.Where(x => !tagNames.Contains(x.Name)).ToList();

            foreach (var deletedTag in deletedTags)
            {
                jobAdvert.RemoveTag(deletedTag);
            }

            foreach (var addedTag in addedTags)
            {
                jobAdvert.AddTag(Tag.Create(addedTag));
            }

            await _jobAdvertRepository.UpdateAsync(jobAdvert.Id, jobAdvert);
            return jobAdvert;
        }
    }
}