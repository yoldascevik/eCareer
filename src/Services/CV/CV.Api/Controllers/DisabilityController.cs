using System.Threading.Tasks;
using CurriculumVitae.Api.Controllers.Base;
using CurriculumVitae.Application.Disability.Commands.Add;
using CurriculumVitae.Application.Disability.Dtos;
using CurriculumVitae.Application.Disability.Queries.Get;
using CurriculumVitae.Application.Disability.Queries.GetById;
using CurriculumVitae.Application.PersonalInfo.Commands.Update;
using CurriculumVitae.Application.PersonalInfo.Dtos;
using CurriculumVitae.Application.PersonalInfo.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Api.Controllers
{
    [Route("api/cv/{cvId}/personal")]
    public class DisabilitiyController : CVApiControllerBase
    {
        private readonly IMediator _mediator;

        public DisabilitiyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get person disability info
        /// </summary>
        [HttpGet("disabilities")]
        public async Task<IActionResult> Get(string cvId)
            => Ok(await _mediator.Send(new GetDisabilitiesQuery(cvId)));
        
        /// <summary>
        /// Get person disability by id
        /// </summary>
        [HttpGet("disabilities/{id}")]
        public async Task<IActionResult> Get(string cvId, string id)
            => Ok(await _mediator.Send(new GetDisabilityByIdQuery(cvId, id)));

        /// <summary>
        /// Add new disability to personal info
        /// </summary>
        /// <param name="cvId">Cv Id</param>
        /// <param name="disabilityInfo">Disability Info</param>
        [HttpPost]
        public async Task<IActionResult> Create(string cvId, [FromBody] DisabilityInputDto disabilityInfo)
        {
            var disability = await _mediator.Send(new AddDisabilityCommand(cvId, disabilityInfo));
            return CreatedAtAction(nameof(Get), new { cvId, disability.Id}, disability);
        }

    }
}