using System.Threading.Tasks;
using CurriculumVitae.Api.Controllers.Base;
using CurriculumVitae.Application.PersonalInfo.Commands.Update;
using CurriculumVitae.Application.PersonalInfo.Dtos;
using CurriculumVitae.Application.PersonalInfo.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Api.Controllers
{
    [Route("api/cv")]
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
        /// <param name="id">CV id</param>
        [HttpGet("{id}/personal")]
        public async Task<IActionResult> GetPersonalInfo(string id)
            => Ok(await _mediator.Send(new GetPersonalInfoQuery(id)));

        /// <summary>
        /// Update personal info
        /// </summary>
        /// <param name="id">CV id</param>
        /// <param name="personalInfo">Personal Info</param>
        [HttpPut("{id}/personal")]
        public async Task<IActionResult> UpdatePersonalInfo(string id, [FromBody] PersonalInfoDto personalInfo)
            => Ok(await _mediator.Send(new UpdatePersonalInfoCommand(id, personalInfo)));
    }
}