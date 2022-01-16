using Career.Data.Pagination;
using Job.Api.Constants;
using Job.Api.Controllers.Base;
using Job.Application.Candidate.Dtos;
using Job.Application.Candidate.Queries.GetByJobId;
using Job.Application.Job.Commands.AddEducationLevel;
using Job.Application.Job.Commands.AddLocation;
using Job.Application.Job.Commands.AddWorkType;
using Job.Application.Job.Commands.Apply;
using Job.Application.Job.Commands.Create;
using Job.Application.Job.Commands.Delete;
using Job.Application.Job.Commands.Publish;
using Job.Application.Job.Commands.RemoveEducationLevel;
using Job.Application.Job.Commands.RemoveLocation;
using Job.Application.Job.Commands.RemoveWorkType;
using Job.Application.Job.Commands.Revoke;
using Job.Application.Job.Commands.SendForApproval;
using Job.Application.Job.Commands.Update;
using Job.Application.Job.Commands.UpdateJobTags;
using Job.Application.Job.Dtos;
using Job.Application.Job.Queries.Get;
using Job.Application.Job.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job.Api.Controllers;

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
    /// Get applied candidates
    /// </summary>
    /// <param name="id">Job id</param>
    /// <param name="includeDeactivated">Include deactivated candidates</param>
    /// <param name="paginationFilter">Pagination options</param>
    [HttpGet("{id}/candidates")]
    public async Task<IActionResult> GetCandidates(Guid id, bool includeDeactivated, [FromQuery] PaginationFilter paginationFilter)
        => Ok(await _mediator.Send(new GetCandidatesByJobIdQuery(id, includeDeactivated, paginationFilter)));
        
    /// <summary>
    /// Create new job
    /// </summary>
    /// <returns>Created job url</returns>
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.ManageJob)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateJobCommand request)
    {
        Guid jobId = await _mediator.Send(request);

        return CreatedAtAction(nameof(Get), new {id = jobId},null);
    }

    /// <summary>
    /// Update job info
    /// </summary>
    /// <param name="id">Job id to be updated</param>
    /// <param name="jobInfo">Job info</param>
    [HttpPut("{id}")]
    [Authorize(Policy = AuthorizationPolicies.ManageJob)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] JobInputDto jobInfo)
        => Ok(await _mediator.Send(new UpdateJobCommand(id, jobInfo)));

    /// <summary>
    /// Delete existing job
    /// </summary>
    /// <param name="id">Job id to be deleted</param>
    [HttpDelete("{id}")]
    [Authorize(Policy = AuthorizationPolicies.ManageJob)]
    public async Task DeleteAsync(Guid id)
        => Ok(await _mediator.Send(new DeleteJobCommand(id)));

    /// <summary>
    /// Publish job
    /// </summary>
    /// <param name="id">Job id</param>
    /// <param name="validityDate">Job validity date</param>
    [HttpPut("{id}/publish")]
    [Authorize(Policy = AuthorizationPolicies.PublishJob)]
    public async Task<IActionResult> PublishAsync(Guid id, [FromBody] DateTime validityDate)
        => Ok(await _mediator.Send(new PublishJobCommand(id, validityDate)));

    /// <summary>
    /// Revoke job
    /// </summary>
    /// <param name="id">Job id</param>
    /// <param name="reason">Reason of job revoke</param>
    [HttpPut("{id}/revoke")]
    [Authorize(Policy = AuthorizationPolicies.ManageJob)]
    public async Task<IActionResult> RevokeAsync(Guid id, [FromBody] string reason)
        => Ok(await _mediator.Send(new RevokeJobCommand(id, reason)));

    /// <summary>
    /// Send job for approval before publish
    /// </summary>
    /// <param name="id">Job id</param>
    [HttpPut("{id}/send-for-approval")]
    [Authorize(Policy = AuthorizationPolicies.ManageJob)]
    public async Task<IActionResult> SendForApprovalAsync(Guid id)
        => Ok(await _mediator.Send(new SendJobForApprovalCommand(id)));

    /// <summary>
    /// Apply for a candidate
    /// </summary>
    /// <param name="id">Job id</param>
    /// <param name="candidate">Candidate info</param>
    [HttpPost("{id}/apply")]
    [Authorize(Policy = AuthorizationPolicies.Candidate)]
    public async Task<IActionResult> Apply(Guid id, [FromBody] CandidateInputDto candidate)
        => Ok(await _mediator.Send(new ApplyCommand(id, candidate)));

    /// <summary>
    /// Add new job location
    /// </summary>
    /// <param name="id">Job id</param>
    /// <param name="location">Location info</param>
    [HttpPost("{id}/locations")]
    [Authorize(Policy = AuthorizationPolicies.ManageJob)]
    public async Task<IActionResult> AddLocation(Guid id, [FromBody] JobLocationInputDto location)
        => Ok(await _mediator.Send(new AddLocationCommand(id, location)));

    /// <summary>
    /// Remove job location
    /// </summary>
    /// <param name="id">Job id</param>
    /// <param name="locationId">Job location id</param>
    [HttpDelete("{id}/locations/{locationId}")]
    [Authorize(Policy = AuthorizationPolicies.ManageJob)]
    public async Task<IActionResult> RemoveLocation(Guid id, Guid locationId)
        => Ok(await _mediator.Send(new RemoveLocationCommand(id, locationId)));

    /// <summary>
    /// Add new work type to job
    /// </summary>
    /// <param name="id">Job id</param>
    /// <param name="workType">Work type info</param>
    [HttpPost("{id}/work-types")]
    [Authorize(Policy = AuthorizationPolicies.ManageJob)]
    public async Task<IActionResult> AddWorkType(Guid id, [FromBody] IdNameRefDto workType)
        => Ok(await _mediator.Send(new AddWorkTypeCommand(id, workType)));

    /// <summary>
    /// Remove work type from job
    /// </summary>
    /// <param name="id">Job id</param>
    /// <param name="workTypeId">Work type id</param>
    [HttpDelete("{id}/work-types/{workTypeId}")]
    [Authorize(Policy = AuthorizationPolicies.ManageJob)]
    public async Task<IActionResult> RemoveWorkType(Guid id, string workTypeId)
        => Ok(await _mediator.Send(new RemoveWorkTypeCommand(id, workTypeId)));

    /// <summary>
    /// Add new education level to job
    /// </summary>
    /// <param name="id">Job id</param>
    /// <param name="educationLevel">Education level info</param>
    [HttpPost("{id}/education-levels")]
    [Authorize(Policy = AuthorizationPolicies.ManageJob)]
    public async Task<IActionResult> AddEducationLevel(Guid id, [FromBody] IdNameRefDto educationLevel)
        => Ok(await _mediator.Send(new AddEducationLevelCommand(id, educationLevel)));

    /// <summary>
    /// Remove education level from job
    /// </summary>
    /// <param name="id">Job id</param>
    /// <param name="educationLevelId">Education level id</param>
    [HttpDelete("{id}/education-levels/{educationLevelId}")]
    [Authorize(Policy = AuthorizationPolicies.ManageJob)]
    public async Task<IActionResult> RemoveEducationLevel(Guid id, string educationLevelId)
        => Ok(await _mediator.Send(new RemoveEducationLevelCommand(id, educationLevelId)));

    /// <summary>
    /// Update job tags
    /// </summary>
    /// <param name="id">Job id</param>
    /// <param name="tags">Tag array</param>
    [HttpPut("{id}/tags")]
    [Authorize(Policy = AuthorizationPolicies.ManageJob)]
    public async Task<IActionResult> UpdateJobTags(Guid id, [FromBody] string[] tags)
        => Ok(await _mediator.Send(new UpdateJobTagsCommand(id, tags)));
}