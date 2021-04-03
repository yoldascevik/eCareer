using System;
using System.Threading.Tasks;
using Job.Api.Controllers.Base;
using Job.Application.Candidate.Dtos;
using Job.Application.Job.Commands.AddLocation;
using Job.Application.Job.Commands.AddWorkType;
using Job.Application.Job.Commands.Apply;
using Job.Application.Job.Commands.Create;
using Job.Application.Job.Commands.Delete;
using Job.Application.Job.Commands.Publish;
using Job.Application.Job.Commands.RemoveLocation;
using Job.Application.Job.Commands.RemoveWorkType;
using Job.Application.Job.Commands.Revoke;
using Job.Application.Job.Commands.SendForApproval;
using Job.Application.Job.Commands.Update;
using Job.Application.Job.Dtos;
using Job.Application.Job.Queries.Get;
using Job.Application.Job.Queries.GetById;
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
        
        /// <summary>
        /// Revoke job
        /// </summary>
        /// <param name="id">Job id</param>
        /// <param name="reason">Reason of job revoke</param>
        [HttpPut("{id}/revoke")]
        public async Task<IActionResult> RevokeAsync(Guid id, [FromBody] string reason)
            => Ok(await _mediator.Send(new RevokeJobCommand(id, reason)));
        
        /// <summary>
        /// Send job for approval before publish
        /// </summary>
        /// <param name="id">Job id</param>
        [HttpPut("{id}/send-for-approval")]
        public async Task<IActionResult> SendForApprovalAsync(Guid id)
            => Ok(await _mediator.Send(new SendJobForApprovalCommand(id)));

        /// <summary>
        /// Apply for a candidate
        /// </summary>
        /// <param name="id">Job id</param>
        /// <param name="candidate">Candidate info</param>
        [HttpPost("{id}/apply")]
        public async Task<IActionResult> Apply(Guid id, [FromBody] CandidateInputDto candidate)
            => Ok(await _mediator.Send(new ApplyCommand(id, candidate)));

        /// <summary>
        /// Add new job location
        /// </summary>
        /// <param name="id">Job id</param>
        /// <param name="countryId">Country id</param>
        /// <param name="cityId">City id</param>
        /// <param name="districtId">District id</param>
        [HttpPost("{id}/locations")]
        public async Task<IActionResult> AddLocation(Guid id, [FromBody] string countryId, [FromBody] string cityId, [FromBody] string districtId)
            => Ok(await _mediator.Send(new AddLocationCommand(id, countryId, cityId, districtId)));
        
        /// <summary>
        /// Remove job location
        /// </summary>
        /// <param name="id">Job id</param>
        /// <param name="locationId">Job location id</param>
        [HttpDelete("{id}/locations/{locationId}")]
        public async Task<IActionResult> RemoveLocation(Guid id, Guid locationId)
            => Ok(await _mediator.Send(new RemoveLocationCommand(id, locationId)));
        
        /// <summary>
        /// Add new work type to job
        /// </summary>
        /// <param name="id">Job id</param>
        /// <param name="workTypeId">Work type id</param>
        /// <param name="name">Work type name</param>
        [HttpPost("{id}/work-types")]
        public async Task<IActionResult> AddWorkType(Guid id, [FromBody] string workTypeId, [FromBody] string name)
            => Ok(await _mediator.Send(new AddWorkTypeCommand(id, workTypeId, name)));
        
        /// <summary>
        /// Remove work type from job
        /// </summary>
        /// <param name="id">Job id</param>
        /// <param name="workTypeId">Work type id</param>
        [HttpDelete("{id}/work-types/{workTypeId}")]
        public async Task<IActionResult> RemoveLocation(Guid id, string workTypeId)
            => Ok(await _mediator.Send(new RemoveWorkTypeCommand(id, workTypeId)));
    }
}