using System.Threading.Tasks;
using CurriculumVitae.Api.Controllers.Base;
using CurriculumVitae.Application.CoverLetter.Commands.Create;
using CurriculumVitae.Application.CoverLetter.Commands.Delete;
using CurriculumVitae.Application.CoverLetter.Commands.Update;
using CurriculumVitae.Application.CoverLetter.Dtos;
using CurriculumVitae.Application.CoverLetter.Queries.GetById;
using CurriculumVitae.Application.CoverLetter.Queries.GetByUserId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Api.Controllers
{
    [Route("api/cover-letters")]
    public class CoverLetterController : CVApiControllerBase
    {
        private readonly IMediator _mediator;

        public CoverLetterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get specific cover letter by id
        /// </summary>
        /// <param name="id">Cover letter id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
            => Ok(await _mediator.Send(new GetCoverLetterByIdQuery(id)));

        /// <summary>
        /// Get cover letters by user
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCoverLettersByUserIdQuery request)
            => Ok(await _mediator.Send(request));

        /// <summary>
        /// Create new cover letter
        /// </summary>
        /// <param name="request">Cover letter Info</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCoverLetterCommand request)
        {
            CoverLetterDto coverLetterDto = await _mediator.Send(request);
            return CreatedAtAction(nameof(Get), new {id = coverLetterDto.Id}, coverLetterDto);
        }

        /// <summary>
        /// Update cover letter
        /// </summary>
        /// <param name="id">Cover letter id</param>
        /// <param name="coverLetter">Cover letter info</param>
        [HttpPut("{id}")]
        public async Task Update(string id, [FromBody] CoverLetterInputDto coverLetter)
            => Ok(await _mediator.Send(new UpdateCoverLetterCommand(id, coverLetter)));

        /// <summary>
        /// Delete cover letter
        /// </summary>
        /// <param name="id">Cover letter id</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
            => Ok(await _mediator.Send(new DeleteCoverLetterCommand(id)));
    }
}