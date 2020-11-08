using System.Threading.Tasks;
using Career.Utilities.Pagination;
using Definition.Api.Controllers.Base;
using Definition.Application.Location.City;
using Definition.Application.Location.District;
using Microsoft.AspNetCore.Mvc;

namespace Definition.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/location/city")]
    public class CityController : DefinitionApiController
    {
        private readonly ICityService _cityService;
        private readonly IDistrictService _districtService;

        public CityController(ICityService cityService, IDistrictService districtService)
        {
            _cityService = cityService;
            _districtService = districtService;
        }

        /// <summary>
        /// Get all cities
        /// </summary>
        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter)
            => Ok(await _cityService.GetAsync(paginationFilter));
        
        /// <summary>
        /// Get districts of city
        /// </summary>
        /// <param name="id">City id</param>
        /// <param name="paginationFilter">Pagination configuration</param>
        [HttpGet("{id}/districts")]
        public virtual async Task<IActionResult> Get(string id, [FromQuery] PaginationFilter paginationFilter)
            => Ok(await _districtService.GetByCityId(id, paginationFilter));

        /// <summary>
        /// Get specific city by id
        /// </summary>
        /// <param name="id">City id</param>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(string id)
            => Ok(await _cityService.GetByIdAsync(id));

        /// <summary>
        /// Create new City
        /// </summary>
        /// <param name="request">City info</param>
        /// <returns>Created city info</returns>
        [HttpPost]
        public virtual async Task<CityDto> CreateAsync([FromBody] CityRequestModel request)
            => await _cityService.CreateAsync(request);

        /// <summary>
        /// Update existing city
        /// </summary>
        /// <param name="id">City id to be updaed</param>
        /// <param name="request">City info</param>
        /// <returns>Updated city info</returns>
        [HttpPut("{id}")]
        public virtual async Task<CityDto> UpdateAsync(string id, [FromBody] CityRequestModel request)
            => await _cityService.UpdateAsync(id, request);

        /// <summary>
        /// Delete existing city
        /// </summary>
        /// <param name="id">City id to be deleted</param>
        [HttpDelete("{id}")]
        public virtual async Task DeleteAsync(string id)
            => await _cityService.DeleteAsync(id);
    }
}