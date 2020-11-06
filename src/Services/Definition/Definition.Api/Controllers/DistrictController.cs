using System.Threading.Tasks;
using Career.Utilities.Pagination;
using Definition.Api.Controllers.Base;
using Definition.Application.Location.District;
using Microsoft.AspNetCore.Mvc;

namespace Definition.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/location/[controller]")]
    public class DistrictController : DefinitionApiController
    {
        private readonly IDistrictService _districtService;

        public DistrictController(IDistrictService districtService)
        {
            _districtService = districtService;
        }

        /// <summary>
        /// Get all districts
        /// </summary>
        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter)
            => Ok(await _districtService.GetAsync(paginationFilter));

        /// <summary>
        /// Get specific district by id
        /// </summary>
        /// <param name="id">District id</param>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(string id)
            => Ok(await _districtService.GetByIdAsync(id));

        /// <summary>
        /// Create new District
        /// </summary>
        /// <param name="request">District info</param>
        /// <returns>Created district info</returns>
        [HttpPost]
        public virtual async Task<DistrictDto> CreateAsync([FromBody] DistrictRequestModel request)
            => await _districtService.CreateAsync(request);

        /// <summary>
        /// Update existing district
        /// </summary>
        /// <param name="id">District id to be updaed</param>
        /// <param name="request">District info</param>
        /// <returns>Updated district info</returns>
        [HttpPut("{id}")]
        public virtual async Task<DistrictDto> UpdateAsync(string id, [FromBody] DistrictRequestModel request)
            => await _districtService.UpdateAsync(id, request);

        /// <summary>
        /// Delete existing district
        /// </summary>
        /// <param name="id">District id to be deleted</param>
        [HttpDelete("{id}")]
        public virtual async Task DeleteAsync(string id)
            => await _districtService.DeleteAsync(id);
    }
}