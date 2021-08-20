using System.Threading.Tasks;
using CurriculumVitae.Api.Controllers.Base;
using CurriculumVitae.Application.SocialProfileType.Command.Create;
using CurriculumVitae.Application.SocialProfileType.Command.Delete;
using CurriculumVitae.Application.SocialProfileType.Command.Update;
using CurriculumVitae.Application.SocialProfileType.Dtos;
using CurriculumVitae.Application.SocialProfileType.Queries.Get;
using CurriculumVitae.Application.SocialProfileType.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Api.Controllers
{
    [Route("api/social-profile-types")]
    public class SocialProfileTypeController : CVApiControllerBase
    {
        private readonly IMediator _mediator;

        public SocialProfileTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all social profile types
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetSocialProfileTypesQuery request)
            => Ok(await _mediator.Send(request));

        /// <summary>
        /// Get social profile type by id
        /// </summary>
        /// <param name="id">Social profile type id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
            => Ok(await _mediator.Send(new GetSocialProfileTypeByIdQuery(id)));
        
        /// <summary>
        /// Create new social profile type
        /// </summary>
        /// <param name="request">Type info</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] CreateSocialProfileTypeCommand request)
        {
            SocialProfileTypeDto socialProfileTypeDto = await _mediator.Send(request);
            return CreatedAtAction(nameof(Get), new {id = socialProfileTypeDto.Id}, socialProfileTypeDto);
        }
        
        /// <summary>
        /// Update social profile type
        /// </summary>
        /// <param name="id">Social profile type id</param>
        /// <param name="socialProfileType">Social profile type info</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SocialProfileTypeInputDto socialProfileType)
            => Ok(await _mediator.Send(new UpdateSocialProfileTypeCommand(id, socialProfileType)));
        
        /// <summary>
        /// Delete social profile type
        /// </summary>
        /// <param name="id">Social profile type id</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
            => Ok(await _mediator.Send(new DeleteSocialProfileTypeCommand(id)));
    }
}