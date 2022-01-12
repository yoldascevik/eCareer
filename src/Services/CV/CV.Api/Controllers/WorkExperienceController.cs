using System.Threading.Tasks;
using CurriculumVitae.Api.Constants;
using CurriculumVitae.Api.Controllers.Base;
using CurriculumVitae.Application.WorkExperience.Commands.Add;
using CurriculumVitae.Application.WorkExperience.Commands.Delete;
using CurriculumVitae.Application.WorkExperience.Commands.Update;
using CurriculumVitae.Application.WorkExperience.Dtos;
using CurriculumVitae.Application.WorkExperience.Queries.Get;
using CurriculumVitae.Application.WorkExperience.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Api.Controllers
{
    [Route("api/cv/{cvId}/experiences")]
    public class WorkExperienceController : CVApiControllerBase
    {
        private readonly IMediator _mediator;

        public WorkExperienceController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Get work experiences from cv
        /// </summary>
        /// <param name="cvId">Cv id</param>
        [HttpGet]
        public async Task<IActionResult> Get(string cvId)
            => Ok(await _mediator.Send(new GetWorkExperiencesQuery(cvId)));
        
        /// <summary>
        /// Get work experience by id
        /// </summary>
        /// <param name="cvId">Cv id</param>
        /// <param name="id">Work experience id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string cvId, string id)
            => Ok(await _mediator.Send(new GetWorkExperienceByIdQuery(cvId, id)));
        
        /// <summary>
        /// Add new work experience
        /// </summary>
        /// <param name="cvId">Cv id</param>
        /// <param name="workExperienceInfo">Work experience info</param>
        [HttpPost]
        [Authorize(Policy = AuthorizationPolicies.ManageCv)]
        public async Task<IActionResult> Create(string cvId, [FromBody] WorkExperienceInputDto workExperienceInfo)
        {
            var workExperience = await _mediator.Send(new AddWorkExperienceCommand(cvId, workExperienceInfo));
            return CreatedAtAction(nameof(GetById), new {cvId, workExperience.Id}, workExperience);
        }
        
        /// <summary>
        /// Update work experience
        /// </summary>
        /// <param name="cvId">Cv id</param>
        /// <param name="id">Work experience id</param>
        /// <param name="workExperience">Work experience info</param>
        [HttpPut("{id}")]
        [Authorize(Policy = AuthorizationPolicies.ManageCv)]
        public async Task<IActionResult> Update(string cvId, string id, [FromBody] WorkExperienceInputDto workExperience)
            => Ok(await _mediator.Send(new UpdateWorkExperienceCommand(cvId, id, workExperience)));
        
        /// <summary>
        /// Delete work experience from cv
        /// </summary>
        /// <param name="cvId">Cv id</param>
        /// <param name="id">Work experience id</param>
        [HttpDelete("{id}")]
        [Authorize(Policy = AuthorizationPolicies.ManageCv)]
        public async Task<IActionResult> Delete(string cvId, string id)
            => Ok(await _mediator.Send(new DeleteWorkExperienceCommand(cvId, id)));
    }
}