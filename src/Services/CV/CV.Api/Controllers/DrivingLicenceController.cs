using System.Threading.Tasks;
using CurriculumVitae.Api.Constants;
using CurriculumVitae.Api.Controllers.Base;
using CurriculumVitae.Application.DrivingLicence.Commands.Add;
using CurriculumVitae.Application.DrivingLicence.Commands.Delete;
using CurriculumVitae.Application.DrivingLicence.Commands.Update;
using CurriculumVitae.Application.DrivingLicence.Dtos;
using CurriculumVitae.Application.DrivingLicence.Queries.Get;
using CurriculumVitae.Application.DrivingLicence.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Api.Controllers;

[Route("api/cv/{cvId}/driving-licences")]
public class DrivingLicenceController : CVApiControllerBase
{
    private readonly IMediator _mediator;

    public DrivingLicenceController(IMediator mediator)
    {
        _mediator = mediator;
    }
        
    /// <summary>
    /// Get driving licences from cv
    /// </summary>
    /// <param name="cvId">Cv id</param>
    [HttpGet]
    public async Task<IActionResult> Get(string cvId)
        => Ok(await _mediator.Send(new GetDrivingLicencesQuery(cvId)));
        
    /// <summary>
    /// Get driving licence by id
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="id">Driving licence id</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string cvId, string id)
        => Ok(await _mediator.Send(new GetDrivingLicenceByIdQuery(cvId, id)));
        
    /// <summary>
    /// Add new driving licence
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="drivingLicenceInfo">Driving licence info</param>
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task<IActionResult> Create(string cvId, [FromBody] DrivingLicenceInputDto drivingLicenceInfo)
    {
        var drivingLicence = await _mediator.Send(new AddDrivingLicenceCommand(cvId, drivingLicenceInfo));
        return CreatedAtAction(nameof(GetById), new {cvId, drivingLicence.Id}, drivingLicence);
    }
        
    /// <summary>
    /// Update driving licence
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="id">Driving licence id</param>
    /// <param name="drivingLicence">Driving licence info</param>
    [HttpPut("{id}")]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task<IActionResult> Update(string cvId, string id, [FromBody] DrivingLicenceInputDto drivingLicence)
        => Ok(await _mediator.Send(new UpdateDrivingLicenceCommand(cvId, id, drivingLicence)));
        
    /// <summary>
    /// Delete driving licence from cv
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="id">Driving licence id</param>
    [HttpDelete("{id}")]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task<IActionResult> Delete(string cvId, string id)
        => Ok(await _mediator.Send(new DeleteDrivingLicenceCommand(cvId, id)));
}