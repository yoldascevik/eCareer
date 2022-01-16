using CurriculumVitae.Api.Constants;
using CurriculumVitae.Api.Controllers.Base;
using CurriculumVitae.Application.Attachment.Commands.Create;
using CurriculumVitae.Application.Attachment.Commands.Delete;
using CurriculumVitae.Application.Attachment.Commands.Update;
using CurriculumVitae.Application.Attachment.Dtos;
using CurriculumVitae.Application.Attachment.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Api.Controllers;

[Route("api/attachments")]
public class AttachmentController : CVApiControllerBase
{
    private readonly IMediator _mediator;

    public AttachmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get specific attachment by id
    /// </summary>
    /// <param name="id">Attachment id</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
        => Ok(await _mediator.Send(new GetAttachmentByIdQuery(id)));

    /// <summary>
    /// Create new attachment
    /// </summary>
    /// <param name="request">Attachment Info</param>
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task<IActionResult> Create([FromBody] CreateAttachmentCommand request)
    {
        AttachmentDto attachmentDto = await _mediator.Send(request);
        return CreatedAtAction(nameof(Get), new {id = attachmentDto.Id}, attachmentDto);
    }

    /// <summary>
    /// Update attachment
    /// </summary>
    /// <param name="id">Attachment id</param>
    /// <param name="attachment">Attachment info</param>
    [HttpPut("{id}")]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task Update(string id, [FromBody] AttachmentInputDto attachment)
        => Ok(await _mediator.Send(new UpdateAttachmentCommand(id, attachment)));

    /// <summary>
    /// Delete attachment
    /// </summary>
    /// <param name="id">Attachment id</param>
    [HttpDelete("{id}")]
    [Authorize(Policy = AuthorizationPolicies.ManageCv)]
    public async Task<IActionResult> Delete(string id)
        => Ok(await _mediator.Send(new DeleteAttachmentCommand(id)));
}