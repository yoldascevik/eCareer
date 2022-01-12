using System.Threading.Tasks;
using CurriculumVitae.Api.Constants;
using CurriculumVitae.Api.Controllers.Base;
using CurriculumVitae.Application.DisabilityType;
using CurriculumVitae.Application.DisabilityType.Commands.Create;
using CurriculumVitae.Application.DisabilityType.Commands.Delete;
using CurriculumVitae.Application.DisabilityType.Commands.Update;
using CurriculumVitae.Application.DisabilityType.Queries.Get;
using CurriculumVitae.Application.DisabilityType.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Api.Controllers
{
    [Route("api/disability-types")]
    public class DisabilityTypeController : CVApiControllerBase
    {
        private readonly IMediator _mediator;

        public DisabilityTypeController(IMediator mediator)
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
        /// Get disability type by id
        /// </summary>
        /// <param name="id">Disability type id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
            => Ok(await _mediator.Send(new GetDisabilityTypeByIdQuery(id)));
        
        /// <summary>
        /// Create new disability type
        /// </summary>
        /// <param name="request">Disability type info</param>
        [HttpPost]
        [Authorize(Policy = AuthorizationPolicies.ManageCv)]
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
        [Authorize(Policy = AuthorizationPolicies.ManageCv)]
        public async Task<IActionResult> Update(string id, string name)
            => Ok(await _mediator.Send(new UpdateDisabilityTypeCommand(id, name)));

        /// <summary>
        /// Delete disability type
        /// </summary>
        /// <param name="id">Disability type id</param>
        [HttpDelete("{id}")]
        [Authorize(Policy = AuthorizationPolicies.ManageCv)]
        public async Task<IActionResult> Delete(string id)
            => Ok(await _mediator.Send(new DeleteDisabilityTypeCommand(id)));
    }
}