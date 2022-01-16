using System.Threading.Tasks;
using CurriculumVitae.Api.Constants;
using CurriculumVitae.Api.Controllers.Base;
using CurriculumVitae.Application.SocialProfile.Commands.Add;
using CurriculumVitae.Application.SocialProfile.Commands.Delete;
using CurriculumVitae.Application.SocialProfile.Commands.Update;
using CurriculumVitae.Application.SocialProfile.Dtos;
using CurriculumVitae.Application.SocialProfile.Queries.Get;
using CurriculumVitae.Application.SocialProfile.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Api.Controllers;

[Route("api/cv/{cvId}/social")]
public class SocialProfileController : CVApiControllerBase
{
    private readonly IMediator _mediator;

    public SocialProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }
        
    /// <summary>
    /// Get social profiles from cv
    /// </summary>
    /// <param name="cvId">Cv id</param>
    [HttpGet]
    public async Task<IActionResult> Get(string cvId)
        => Ok(await _mediator.Send(new GetSocialProfilesQuery(cvId)));
        
    /// <summary>
    /// Get social profile by id
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="id">Social profile id</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string cvId, string id)
        => Ok(await _mediator.Send(new GetSocialProfileByIdQuery(cvId, id)));
        
    /// <summary>
    /// Add new social profile
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="socialProfileInfo">Social profile info</param>
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task<IActionResult> Create(string cvId, [FromBody] SocialProfileInputDto socialProfileInfo)
    {
        var socialProfile = await _mediator.Send(new AddSocialProfileCommand(cvId, socialProfileInfo));
        return CreatedAtAction(nameof(GetById), new {cvId, socialProfile.Id}, socialProfile);
    }
        
    /// <summary>
    /// Update social profile
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="id">Social profile id</param>
    /// <param name="socialProfile">Social profile info</param>
    [HttpPut("{id}")]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task<IActionResult> Update(string cvId, string id, [FromBody] SocialProfileInputDto socialProfile)
        => Ok(await _mediator.Send(new UpdateSocialProfileCommand(cvId, id, socialProfile)));
        
    /// <summary>
    /// Delete social profile from cv
    /// </summary>
    /// <param name="cvId">Cv id</param>
    /// <param name="id">Social profile id</param>
    [HttpDelete("{id}")]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task<IActionResult> Delete(string cvId, string id)
        => Ok(await _mediator.Send(new DeleteSocialProfileCommand(cvId, id)));
}