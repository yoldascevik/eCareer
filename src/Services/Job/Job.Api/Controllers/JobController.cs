using System;
using System.Threading.Tasks;
using Job.Api.Controllers.Base;
using Job.Domain.JobAdvertAggregate;
using Job.Domain.JobAdvertAggregate.Repositories;
using Job.Domain.JobApplicationAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Job.Api.Controllers
{
    [Route("api/jobs")]
    public class JobController: JobApiController
    {
        private readonly IJobAdvertRepository _jobAdvertRepository;

        public JobController(IJobAdvertRepository jobAdvertRepository)
        {
            _jobAdvertRepository = jobAdvertRepository;
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var jobAdvert = JobAdvert.Create(
                Guid.NewGuid(),
                "Test",
                "description",
                "sectorid",
                "positionid",
                "languageid"
            );

            await _jobAdvertRepository.AddAsync(jobAdvert);
            return Ok(jobAdvert.Id);
        }
        
        [HttpGet("Apply/{jobAdvertId}")]
        public async Task<IActionResult> Apply(Guid jobAdvertId)
        {
            var jobAdvert = await _jobAdvertRepository.GetByKeyAsync(jobAdvertId);
            var application = JobApplication.Create(
                jobAdvert,
                Guid.NewGuid(),
                Guid.NewGuid(), 
                "cover letter",
                "channel",
                "referance"
            );
            
            jobAdvert.Apply(application);
            await _jobAdvertRepository.UpdateAsync(jobAdvert.Id, jobAdvert);
            
            return Ok(application.Id);
        }
    }
}