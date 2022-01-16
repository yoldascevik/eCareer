using System.Threading.Tasks;
using CurriculumVitae.Api.Constants;
using CurriculumVitae.Api.Controllers.Base;
using CurriculumVitae.Application.Education.Commands.Add;
using CurriculumVitae.Application.Education.Commands.Delete;
using CurriculumVitae.Application.Education.Commands.Update;
using CurriculumVitae.Application.Education.Dtos;
using CurriculumVitae.Application.Education.Queries.Get;
using CurriculumVitae.Application.Education.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Api.Controllers;

[Route("api/cv/{cvId}/educations")]
public class EducationController : CVApiControllerBase
{
    private readonly IMediator _mediator;

    public EducationController(IMediator mediator)
    {
        _mediator = mediator;
    }
        
    /// <summary>
    /// Get educations from cv
    /// </summary>
    /// <param name="cvId">Cv id</param>
    [HttpGet]
    public async Task<IActionResult> Get(string cvId)
        => Ok(await _mediator.Send(new GetEducationsQuery(cvId)));
        
    /// <summary>
    /// Get education by id
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="id">Education id</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string cvId, string id)
        => Ok(await _mediator.Send(new GetEducationByIdQuery(cvId, id)));
        
    /// <summary>
    /// Add new education
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="educationInfo">Education info</param>
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task<IActionResult> Create(string cvId, [FromBody] EducationInputDto educationInfo)
    {
        var education = await _mediator.Send(new AddEducationCommand(cvId, educationInfo));
        return CreatedAtAction(nameof(GetById), new {cvId, education.Id}, education);
    }
        
    /// <summary>
    /// Update education
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="id">Education id</param>
    /// <param name="education">Education info</param>
    [HttpPut("{id}")]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task<IActionResult> Update(string cvId, string id, [FromBody] EducationInputDto education)
        => Ok(await _mediator.Send(new UpdateEducationCommand(cvId, id, education)));
        
    /// <summary>
    /// Delete education from cv
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="id">Education id</param>
    [HttpDelete("{id}")]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task<IActionResult> Delete(string cvId, string id)
        => Ok(await _mediator.Send(new DeleteEducationCommand(cvId, id)));
}