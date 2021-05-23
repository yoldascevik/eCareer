using System.Threading.Tasks;
using CurriculumVitae.Api.Controllers.Base;
using CurriculumVitae.Application.PersonalInfo.Commands.Update;
using CurriculumVitae.Application.PersonalInfo.Dtos;
using CurriculumVitae.Application.PersonalInfo.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Api.Controllers
{
    [Route("api/cv/{cvId}")]
    public class PersonalInfoController : CVApiControllerBase
    {
        private readonly IMediator _mediator;

        public PersonalInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get personal info from cv
        /// </summary>
        /// <param name="cvId">CV id</param>
        [HttpGet("personal")]
        public async Task<IActionResult> Get(string cvId)
            => Ok(await _mediator.Send(new GetPersonalInfoQuery(cvId)));

        /// <summary>
        /// Update personal info
        /// </summary>
        /// <param name="cvId">CV id</param>
        /// <param name="personalInfo">Personal Info</param>
        [HttpPut("personal")]
        public async Task<IActionResult> Update(string cvId, [FromBody] PersonalInfoDto personalInfo)
            => Ok(await _mediator.Send(new UpdatePersonalInfoCommand(cvId, personalInfo)));
    }
}