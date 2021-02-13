using System;
using System.Threading.Tasks;
using Career.Mongo.IdGenerator;
using Career.Mongo.Repository.Contracts;
using Job.Api.Controllers.Base;
using Job.Domain.Constants;
using Job.Domain.JobAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Job.Api.Controllers
{
    [Route("api/jobs")]
    public class JobController: JobApiController
    {
        private readonly IMongoRepository<JobAdvert> _jobAdvertRepository;

        public JobController(IMongoRepository<JobAdvert> jobAdvertRepository)
        {
            _jobAdvertRepository = jobAdvertRepository;
        }

        [HttpGet("Test")]
        public async Task<IActionResult> Test()
        {
            var objectIdGenerator = new ObjectIdGenerator();
            // var jobAdvertId = new JobAdvertId(objectIdGenerator.Generate().ToString());
            var jobAdvert = JobAdvert.Create(
                // objectIdGenerator.Generate().ToString(),
                Guid.NewGuid(),
                "123123",
                "34234323",
                "we3434",
                "Test Job Advert",
                "",
                20,
                true,
                GenderType.Unspecified,
                DateTime.Now,
                5,
                2);

            await _jobAdvertRepository.AddAsync(jobAdvert);
            return Ok();
        }
    }
}