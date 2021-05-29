using System.Threading.Tasks;
using CurriculumVitae.Api.Controllers.Base;
using CurriculumVitae.Application.DisabilityType;
using CurriculumVitae.Application.DisabilityType.Commands.Create;
using CurriculumVitae.Application.DisabilityType.Commands.Delete;
using CurriculumVitae.Application.DisabilityType.Commands.Update;
using CurriculumVitae.Application.DisabilityType.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Api.Controllers
{
    [Route("api/disability-types")]
    public class DisabilityType : CVApiControllerBase
    {
        private readonly IMediator _mediator;

        public DisabilityType(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all disability types
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetDisabilityTypesQuery request)
            => Ok(await _mediator.Send(request));


        /// <summary>
        /// Create new disability type
        /// </summary>
        /// <param name="request">Disability type info</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] CreateDisabilityTypeCommand request)
        {
            DisabilityTypeDto disabilityTypeDto = await _mediator.Send(request);
            return CreatedAtAction(nameof(Get), new {id = disabilityTypeDto.Id}, disabilityTypeDto);
        }

        /// <summary>
        /// Update disability type
        /// </summary>
        /// <param name="id">Disability type id</param>
        /// <param name="name">Disability type name</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Delete(string id, string name)
            => Ok(await _mediator.Send(new UpdateDisabilityTypeCommand(id, name)));

        /// <summary>
        /// Delete disability type
        /// </summary>
        /// <param name="id">Disability type id</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
            => Ok(await _mediator.Send(new DeleteDisabilityTypeCommand(id)));
    }
}