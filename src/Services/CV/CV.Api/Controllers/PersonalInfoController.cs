using System.Threading.Tasks;
using CurriculumVitae.Api.Constants;
using CurriculumVitae.Api.Controllers.Base;
using CurriculumVitae.Application.PersonalInfo.Commands.AddDisability;
using CurriculumVitae.Application.PersonalInfo.Commands.DeleteDisability;
using CurriculumVitae.Application.PersonalInfo.Commands.Update;
using CurriculumVitae.Application.PersonalInfo.Commands.UpdateDisability;
using CurriculumVitae.Application.PersonalInfo.Dtos;
using CurriculumVitae.Application.PersonalInfo.Queries.Get;
using CurriculumVitae.Application.PersonalInfo.Queries.GetDisabilities;
using CurriculumVitae.Application.PersonalInfo.Queries.GetDisabilityById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = AuthorizationPolicies.ManageCv)]
        public async Task<IActionResult> Update(string cvId, [FromBody] PersonalInfoInputDto personalInfo)
            => Ok(await _mediator.Send(new UpdatePersonalInfoCommand(cvId, personalInfo)));

        /// <summary>
        /// Get person disability info
        /// </summary>
        [HttpGet("personal/disabilities")]
        public async Task<IActionResult> GetDisability(string cvId)
            => Ok(await _mediator.Send(new GetDisabilitiesQuery(cvId)));

        /// <summary>
        /// Get person disability by id
        /// </summary>
        [HttpGet("personal/disabilities/{id}")]
        public async Task<IActionResult> GetDisabilityById(string cvId, string id)
            => Ok(await _mediator.Send(new GetDisabilityByIdQuery(cvId, id)));

        /// <summary>
        /// Add new disability to personal info
        /// </summary>
        /// <param name="cvId">Cv Id</param>
        /// <param name="disabilityInfo">Disability Info</param>
        [HttpPost("personal/disabilities")]
        [Authorize(Policy = AuthorizationPolicies.ManageCv)]
        public async Task<IActionResult> CreateDisability(string cvId, [FromBody] DisabilityInputDto disabilityInfo)
        {
            var disability = await _mediator.Send(new AddDisabilityCommand(cvId, disabilityInfo));
            return CreatedAtAction(nameof(GetDisabilityById), new {cvId, disability.Id}, disability);
        }

        /// <summary>
        /// Update person disability
        /// </summary>
        /// <param name="cvId">Cv Id</param>
        /// <param name="id">Disability Id</param>
        /// <param name="disabilityInfo">Disability Info</param>
        [HttpPut("personal/disabilities/{id}")]
        [Authorize(Policy = AuthorizationPolicies.ManageCv)]
        public async Task<IActionResult> UpdateDisability(string cvId, string id, [FromBody] DisabilityInputDto disabilityInfo)
            => Ok(await _mediator.Send(new UpdateDisabilityCommand(cvId, id, disabilityInfo)));

        /// <summary>
        /// Delete person disability
        /// </summary>
        /// <param name="cvId">Cv Id</param>
        /// <param name="id">Disability Id</param>
        [HttpDelete("personal/disabilities/{id}")]
        [Authorize(Policy = AuthorizationPolicies.ManageCv)]
        public async Task<IActionResult> DeleteDisability(string cvId, string id)
            => Ok(await _mediator.Send(new DeleteDisabilityCommand(cvId, id)));
    }
}