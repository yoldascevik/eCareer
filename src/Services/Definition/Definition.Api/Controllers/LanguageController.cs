using System.Threading.Tasks;
using Career.Data.Pagination;
using Definition.Api.Controllers.Base;
using Definition.Application.Language;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace Definition.Api.Controllers
{
    
    [Route("api/languages")]
    public class LanguageController : DefinitionApiController
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }
        
        /// <summary>
        /// Get all languages
        /// </summary>
        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter)
            => Ok(await _languageService.GetAsync(paginationFilter));
        
        /// <summary>
        /// Get specific language by id
        /// </summary>
        /// <param name="id">Language id</param>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(string id)
            => Ok(await _languageService.GetByIdAsync(id));
        
        /// <summary>
        /// Get specific language by culture name
        /// </summary>
        /// <param name="culture">Language culture name. (eg: en-US)</param>
        [HttpGet("culture/{culture}")]
        public virtual async Task<IActionResult> GetByCulture(string culture)
            => Ok(await _languageService.GetByCultureAsync(culture));
        
        /// <summary>
        /// Create new Language
        /// </summary>
        /// <param name="request">Language info</param>
        /// <returns>Created language info</returns>
        [HttpPost]
        public virtual async Task<LanguageDto> CreateAsync([FromBody] LanguageRequestModel request)
            => await _languageService.CreateAsync(request);
        
        /// <summary>
        /// Update existing language
        /// </summary>
        /// <param name="id">Language id to be updaed</param>
        /// <param name="request">Language info</param>
        /// <returns>Updated language info</returns>
        [HttpPut("{id}")]
        public virtual async Task<LanguageDto> UpdateAsync(string id, [FromBody] LanguageRequestModel request)
            => await _languageService.UpdateAsync(id, request);
        
        /// <summary>
        /// Delete existing language
        /// </summary>
        /// <param name="id">Language id to be deleted</param>
        [HttpDelete("{id}")]
        public virtual async Task DeleteAsync(string id)
            => await _languageService.DeleteAsync(id);
    }
}