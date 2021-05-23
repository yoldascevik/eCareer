using System.Threading.Tasks;
using CurriculumVitae.Api.Controllers.Base;
using CurriculumVitae.Application.Cv.Commands.Create;
using CurriculumVitae.Application.Cv.Commands.Delete;
using CurriculumVitae.Application.Cv.Dtos;
using CurriculumVitae.Application.Cv.Queries.Get;
using CurriculumVitae.Application.Cv.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Api.Controllers
{
    [Route("api/cv")]
    public class CVController : CVApiControllerBase
    {
        private readonly IMediator _mediator;

        public CVController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all CVs
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCVsQuery request)
            => Ok(await _mediator.Send(request));
        
        /// <summary>
        /// Get specific CV by id
        /// </summary>
        /// <param name="id">CV id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
            => Ok(await _mediator.Send(new GetCVByIdQuery(id)));

        /// <summary>
        /// Create new CV
        /// </summary>
        /// <param name="request">CV Info</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCVCommand request)
        {
            CVSummaryDto cv = await _mediator.Send(request);
            return CreatedAtAction(nameof(Get), new {id = cv.Id}, cv);
        }

        /// <summary>
        /// Delete CV
        /// </summary>
        /// <param name="id">CV id</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
            => Ok(await _mediator.Send(new DeleteCVCommand(id)));
    }
}