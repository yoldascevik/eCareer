using System.Threading.Tasks;
using Career.Mongo.Repository.Contracts;
using Job.Api.Controllers.Base;
using Job.Domain.JobAdvertAggregate;
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
            return Ok();
        }
    }
}