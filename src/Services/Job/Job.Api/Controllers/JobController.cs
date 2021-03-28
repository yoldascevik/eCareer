using System;
using System.Threading.Tasks;
using Job.Api.Controllers.Base;
using Job.Application.Job.Commands.CreateJob;
using Job.Application.Job.Commands.DeleteJob;
using Job.Application.Job.Commands.PublishJob;
using Job.Application.Job.Commands.UpdateJob;
using Job.Application.Job.Dtos;
using Job.Application.Job.Queries.GetJobById;
using Job.Application.Job.Queries.GetJobs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Job.Api.Controllers
{
    [Route("api/jobs")]
    public class JobController : JobApiController
    {
        private readonly IMediator _mediator;

        public JobController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all jobs
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetJobsQuery request)
            => Ok(await _mediator.Send(request));

        /// <summary>
        /// Get specific job by id
        /// </summary>
        /// <param name="id">Job id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _mediator.Send(new GetJobByIdQuery(id)));

        /// <summary>
        /// Create new job
        /// </summary>
        /// <param name="companyId">Company id</param>
        /// <param name="jobInfo">Job info</param>
        /// <returns>Created job url</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Guid companyId, [FromBody] JobInputDto jobInfo)
        {
            Guid jobId = await _mediator.Send(new CreateJobCommand(companyId, jobInfo));

            return CreatedAtAction(nameof(Get), new {id = jobId});
        }

        /// <summary>
        /// Update job info
        /// </summary>
        /// <param name="id">Job id to be updated</param>
        /// <param name="jobInfo">Job info</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] JobInputDto jobInfo)
            => Ok(await _mediator.Send(new UpdateJobCommand(id, jobInfo)));

        /// <summary>
        /// Delete existing job
        /// </summary>
        /// <param name="id">Job id to be deleted</param>
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
            => Ok(await _mediator.Send(new DeleteJobCommand(id)));

        /// <summary>
        /// Publish job
        /// </summary>
        /// <param name="id">Job id</param>
        /// <param name="validityDate">Job validity date</param>
        [HttpPut("{id}/publish")]
        public async Task<IActionResult> PublishAsync(Guid id, [FromBody] DateTime validityDate)
            => Ok(await _mediator.Send(new PublishJobCommand(id, validityDate)));
    }
}