using System;
using System.Threading.Tasks;
using Job.Api.Constants;
using Job.Api.Controllers.Base;
using Job.Application.Tag.Commands.Create;
using Job.Application.Tag.Commands.Delete;
using Job.Application.Tag.Commands.Update;
using Job.Application.Tag.Queries.Get;
using Job.Application.Tag.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job.Api.Controllers;

[Route("api/tags")]
public class TagController: JobApiController
{
    private readonly IMediator _mediator;

    public TagController(IMediator mediator)
    {
        _mediator = mediator;
    }
        
    /// <summary>
    /// Get all tags
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetTagsQuery request)
        => Ok(await _mediator.Send(request));
        
    /// <summary>
    /// Get specific tag by id
    /// </summary>
    /// <param name="id">Tag id</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
        => Ok(await _mediator.Send(new GetTagByIdQuery(id)));
        
    /// <summary>
    /// Create new tag
    /// </summary>
    /// <returns>Created tag url</returns>
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.ManageJob)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTagCommand tagCommand)
    {
        Guid tagId = await _mediator.Send(tagCommand);
        return CreatedAtAction(nameof(Get), new {id = tagId}, null);
    }

    /// <summary>
    /// Update tag info
    /// </summary>
    /// <param name="id">Tag id to be updated</param>
    /// <param name="name">Tag name</param>
    [HttpPut("{id}")]
    [Authorize(Policy = AuthorizationPolicies.ManageJob)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] string name)
        => Ok(await _mediator.Send(new UpdateTagCommand(id, name)));

    /// <summary>
    /// Delete existing tag
    /// </summary>
    /// <param name="id">Tag id to be deleted</param>
    [HttpDelete("{id}")]
    [Authorize(Policy = AuthorizationPolicies.ManageJob)]
    public async Task DeleteAsync(Guid id)
        => Ok(await _mediator.Send(new DeleteTagCommand(id)));
}