using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Career.Exceptions;
using Job.Domain.JobAggregate.Repositories;
using Job.Domain.TagAggregate;
using Microsoft.Extensions.Logging;

namespace Job.Domain.JobAggregate.Services
{
    public class JobDomainService: IJobDomainService
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<JobDomainService> _logger;
        
        public JobDomainService(IJobRepository jobRepository, ILogger<JobDomainService> logger)
        {
            Check.NotNull(jobRepository, nameof(jobRepository));
            Check.NotNull(logger, nameof(logger));
            
            _jobRepository = jobRepository;
            _logger = logger;
        }
        
        public async Task<Job> UpdateTagsAsync(Job job, IEnumerable<string> tagNames)
        {
            string[] existingJobTags = job.Tags.Select(x=> x.Name).ToArray();
            string[] addedTags = tagNames.Except(existingJobTags, StringComparer.OrdinalIgnoreCase).ToArray();
            List<TagRef> deletedTags = job.Tags.Where(x => !tagNames.Contains(x.Name, StringComparer.OrdinalIgnoreCase)).ToList();

            foreach (var deletedTag in deletedTags)
            {
                job.RemoveTag(deletedTag);
                _logger.LogInformation("Tag {TagName} removed from job: {JobId}", deletedTag.Name, job.Id);
            }

            foreach (var addedTag in addedTags)
            {
                job.AddTag(Tag.Create(addedTag));
                _logger.LogInformation("Tag {TagName} added to job: {JobId}", addedTag, job.Id);
            }

            await _jobRepository.UpdateAsync(job.Id, job);
            return job;
        }
        
        public async Task RemoveTagsFromJobsAsync(Tag tag)
        {
            Check.NotNull(tag, nameof(tag));
            
            IEnumerable<Job> jobsOfTag = await _jobRepository.GetByTagAsync(tag);
            if (!jobsOfTag.Any())
                return;

            foreach (Job job in jobsOfTag)
            {
                job.RemoveTag(tag);
                await _jobRepository.UpdateAsync(job.Id, job);
                
                _logger.LogInformation("Tag {TagName} removed from job: {JobId}", tag.Name, job.Id);
            }
        }

        public async Task UpdateTagNameFromJobsAsync(Tag tag)
        {
            IEnumerable<Job> jobsOfTag = await _jobRepository.GetByTagAsync(tag);
            if (!jobsOfTag.Any())
                return;

            foreach (Job job in jobsOfTag)
            {
                var jobTag = job.Tags.FirstOrDefault(t => t.TagId == tag.Id);
                if (jobTag != null)
                {
                    jobTag.SetName(tag.Name);
                    await _jobRepository.UpdateAsync(job.Id, job);
                    
                    _logger.LogInformation("Tag name {TagName} updated to {NewTagName} for job: {JobId}", jobTag.Name, tag.Name, job.Id);
                }
            }
        }
    }
}