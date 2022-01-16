using CurriculumVitae.Api.Constants;
using CurriculumVitae.Api.Controllers.Base;
using CurriculumVitae.Application.Certificate.Commands.Add;
using CurriculumVitae.Application.Certificate.Commands.Delete;
using CurriculumVitae.Application.Certificate.Commands.Update;
using CurriculumVitae.Application.Certificate.Dtos;
using CurriculumVitae.Application.Certificate.Queries.Get;
using CurriculumVitae.Application.Certificate.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Api.Controllers;

[Route("api/cv/{cvId}/certificate")]
public class CertificateController : CVApiControllerBase
{
    private readonly IMediator _mediator;

    public CertificateController(IMediator mediator)
    {
        _mediator = mediator;
    }
        
    /// <summary>
    /// Get certificates from cv
    /// </summary>
    /// <param name="cvId">Cv id</param>
    [HttpGet]
    public async Task<IActionResult> Get(string cvId)
        => Ok(await _mediator.Send(new GetCertificatesQuery(cvId)));
        
    /// <summary>
    /// Get certificate by id
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="id">Certificate id</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string cvId, string id)
        => Ok(await _mediator.Send(new GetCertificateByIdQuery(cvId, id)));
        
    /// <summary>
    /// Add new certificate
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="certificateInfo">Certificate info</param>
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task<IActionResult> Create(string cvId, [FromBody] CertificateInputDto certificateInfo)
    {
        var certificate = await _mediator.Send(new AddCertificateCommand(cvId, certificateInfo));
        return CreatedAtAction(nameof(GetById), new {cvId, certificate.Id}, certificate);
    }
        
    /// <summary>
    /// Update certificate
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="id">Certificate id</param>
    /// <param name="certificate">Certificate info</param>
    [HttpPut("{id}")]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task<IActionResult> Update(string cvId, string id, [FromBody] CertificateInputDto certificate)
        => Ok(await _mediator.Send(new UpdateCertificateCommand(cvId, id, certificate)));
        
    /// <summary>
    /// Delete certificate from cv
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="id">Certificate id</param>
    [HttpDelete("{id}")]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task<IActionResult> Delete(string cvId, string id)
        => Ok(await _mediator.Send(new DeleteCertificateCommand(cvId, id)));
}