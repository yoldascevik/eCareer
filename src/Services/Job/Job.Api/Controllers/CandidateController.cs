using Job.Api.Constants;
using Job.Api.Controllers.Base;
using Job.Application.Candidate.Commands.Withdraw;
using Job.Application.Candidate.Queries.Get;
using Job.Application.Candidate.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job.Api.Controllers;

[Route("api/candidates")]
public class CandidateController: JobApiController
{
    private readonly IMediator _mediator;

    public CandidateController(IMediator mediator)
    {
        _mediator = mediator;
    }
        
    /// <summary>
    /// Get all candidates
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetCandidatesQuery request)
        => Ok(await _mediator.Send(request));
        
    /// <summary>
    /// Get specific candidate by id
    /// </summary>
    /// <param name="id">Candidate id</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
        => Ok(await _mediator.Send(new GetCandidateByIdQuery(id)));
        
    /// <summary>
    /// Withdraw candidate application 
    /// </summary>
    /// <param name="id">Candidate id</param>
    [HttpPut("{id}/withdraw")]
    [Authorize(Policy = AuthorizationPolicies.Candidate)]
    public async Task<IActionResult> Withdraw(Guid id)
        => Ok(await _mediator.Send(new WithdrawCandidateCommand(id)));
}