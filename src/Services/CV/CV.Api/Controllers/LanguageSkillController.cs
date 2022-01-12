using System.Threading.Tasks;
using CurriculumVitae.Api.Constants;
using CurriculumVitae.Api.Controllers.Base;
using CurriculumVitae.Application.LanguageSkill.Commands.Add;
using CurriculumVitae.Application.LanguageSkill.Commands.Delete;
using CurriculumVitae.Application.LanguageSkill.Commands.Update;
using CurriculumVitae.Application.LanguageSkill.Dtos;
using CurriculumVitae.Application.LanguageSkill.Queries.Get;
using CurriculumVitae.Application.LanguageSkill.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Api.Controllers
{
    [Route("api/cv/{cvId}/languages")]
    public class LanguageSkillController : CVApiControllerBase
    {
        private readonly IMediator _mediator;

        public LanguageSkillController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Get language skills from cv
        /// </summary>
        /// <param name="cvId">Cv id</param>
        [HttpGet]
        public async Task<IActionResult> Get(string cvId)
            => Ok(await _mediator.Send(new GetLanguageSkillsQuery(cvId)));
        
        /// <summary>
        /// Get language skill by id
        /// </summary>
        /// <param name="cvId">Cv id</param>
        /// <param name="id">Language skill id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string cvId, string id)
            => Ok(await _mediator.Send(new GetLanguageSkillByIdQuery(cvId, id)));
        
        /// <summary>
        /// Add new language skill
        /// </summary>
        /// <param name="cvId">Cv id</param>
        /// <param name="languageSkillInfo">Language skill info</param>
        [HttpPost]
        [Authorize(Policy = AuthorizationPolicies.ManageCv)]
        public async Task<IActionResult> Create(string cvId, [FromBody] LanguageSkillInputDto languageSkillInfo)
        {
            var languageSkill = await _mediator.Send(new AddLanguageSkillCommand(cvId, languageSkillInfo));
            return CreatedAtAction(nameof(GetById), new {cvId, languageSkill.Id}, languageSkill);
        }
        
        /// <summary>
        /// Update language skill
        /// </summary>
        /// <param name="cvId">Cv id</param>
        /// <param name="id">Language skill id</param>
        /// <param name="languageSkill">Language skill info</param>
        [HttpPut("{id}")]
        [Authorize(Policy = AuthorizationPolicies.ManageCv)]
        public async Task<IActionResult> Update(string cvId, string id, [FromBody] UpdateLanguageSkillDto languageSkill)
            => Ok(await _mediator.Send(new UpdateLanguageSkillCommand(cvId, id, languageSkill)));
        
        /// <summary>
        /// Delete language skill from cv
        /// </summary>
        /// <param name="cvId">Cv id</param>
        /// <param name="id">Language skill id</param>
        [HttpDelete("{id}")]
        [Authorize(Policy = AuthorizationPolicies.ManageCv)]
        public async Task<IActionResult> Delete(string cvId, string id)
            => Ok(await _mediator.Send(new DeleteLanguageSkillCommand(cvId, id)));
    }
}