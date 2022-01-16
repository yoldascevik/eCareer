using System.Threading.Tasks;
using CurriculumVitae.Api.Constants;
using CurriculumVitae.Api.Controllers.Base;
using CurriculumVitae.Application.Reference.Commands.Add;
using CurriculumVitae.Application.Reference.Commands.Delete;
using CurriculumVitae.Application.Reference.Commands.Update;
using CurriculumVitae.Application.Reference.Dtos;
using CurriculumVitae.Application.Reference.Queries.Get;
using CurriculumVitae.Application.Reference.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Api.Controllers;

[Route("api/cv/{cvId}/references")]
public class ReferenceController : CVApiControllerBase
{
    private readonly IMediator _mediator;

    public ReferenceController(IMediator mediator)
    {
        _mediator = mediator;
    }
        
    /// <summary>
    /// Get references from cv
    /// </summary>
    /// <param name="cvId">Cv id</param>
    [HttpGet]
    public async Task<IActionResult> Get(string cvId)
        => Ok(await _mediator.Send(new GetReferencesQuery(cvId)));
        
    /// <summary>
    /// Get reference by id
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="id">Reference id</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string cvId, string id)
        => Ok(await _mediator.Send(new GetReferenceByIdQuery(cvId, id)));
        
    /// <summary>
    /// Add new reference
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="referenceInfo">Reference info</param>
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task<IActionResult> Create(string cvId, [FromBody] ReferenceInputDto referenceInfo)
    {
        var reference = await _mediator.Send(new AddReferenceCommand(cvId, referenceInfo));
        return CreatedAtAction(nameof(GetById), new {cvId, reference.Id}, reference);
    }
        
    /// <summary>
    /// Update reference
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="id">Reference id</param>
    /// <param name="reference">Reference info</param>
    [HttpPut("{id}")]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task<IActionResult> Update(string cvId, string id, [FromBody] ReferenceInputDto reference)
        => Ok(await _mediator.Send(new UpdateReferenceCommand(cvId, id, reference)));
        
    /// <summary>
    /// Delete reference from cv
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="id">Reference id</param>
    [HttpDelete("{id}")]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task<IActionResult> Delete(string cvId, string id)
        => Ok(await _mediator.Send(new DeleteReferenceCommand(cvId, id)));
}