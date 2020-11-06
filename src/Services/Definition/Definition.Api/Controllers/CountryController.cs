using System.Threading.Tasks;
using Career.Utilities.Pagination;
using Definition.Api.Controllers.Base;
using Definition.Application.Location.Country;
using Microsoft.AspNetCore.Mvc;

namespace Definition.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/location/[controller]")]
    public class CountryController : DefinitionApiController
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        
        /// <summary>
        /// Get all countries
        /// </summary>
        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter)
            => Ok(await _countryService.GetAsync(paginationFilter));
        
        /// <summary>
        /// Get specific country by id
        /// </summary>
        /// <param name="id">Country id</param>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(string id)
            => Ok(await _countryService.GetByIdAsync(id));
        
        /// <summary>
        /// Create new Country
        /// </summary>
        /// <param name="request">Country info</param>
        /// <returns>Created country info</returns>
        [HttpPost]
        public virtual async Task<CountryDto> CreateAsync([FromBody] CountryRequestModel request)
            => await _countryService.CreateAsync(request);
        
        /// <summary>
        /// Update existing country
        /// </summary>
        /// <param name="id">Country id to be updaed</param>
        /// <param name="request">Country info</param>
        /// <returns>Updated country info</returns>
        [HttpPut("{id}")]
        public virtual async Task<CountryDto> UpdateAsync(string id, [FromBody] CountryRequestModel request)
            => await _countryService.UpdateAsync(id, request);
        
        /// <summary>
        /// Delete existing country
        /// </summary>
        /// <param name="id">Country id to be deleted</param>
        [HttpDelete("{id}")]
        public virtual async Task DeleteAsync(string id)
            => await _countryService.DeleteAsync(id);
    }
}