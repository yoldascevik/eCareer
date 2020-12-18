using System.Threading.Tasks;
using Career.Data.Pagination;
using Definition.Api.Controllers.Base;
using Definition.Application.Work.JobPosition;
using Definition.Application.Work.Sector;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace Definition.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/work/sectors")]
    public class SectorController : DefinitionApiController
    {
        private readonly ISectorService _sectorService;
        private readonly IJobPositionService _jobPositionService;

        public SectorController(ISectorService sectorService, IJobPositionService jobPositionService)
        {
            _sectorService = sectorService;
            _jobPositionService = jobPositionService;
        }
        
        /// <summary>
        /// Get all sectors
        /// </summary>
        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter)
            => Ok(await _sectorService.GetAsync(paginationFilter));
        
        /// <summary>
        /// Get job positions of sector
        /// </summary>
        /// <param name="id">Sector id</param>
        /// <param name="paginationFilter">Pagination configuration</param>
        [HttpGet("{id}/positions")]
        public virtual async Task<IActionResult> Get(string id, [FromQuery] PaginationFilter paginationFilter)
            => Ok(await _jobPositionService.GetBySectorId(id, paginationFilter));
        
        /// <summary>
        /// Get specific sector by id
        /// </summary>
        /// <param name="id">Sector id</param>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(string id)
            => Ok(await _sectorService.GetByIdAsync(id));
        
        /// <summary>
        /// Create new sector
        /// </summary>
        /// <param name="request">Sector info</param>
        /// <returns>Created sector info</returns>
        [HttpPost]
        public virtual async Task<SectorDto> CreateAsync([FromBody] SectorRequestModel request)
            => await _sectorService.CreateAsync(request);
        
        /// <summary>
        /// Update existing sector
        /// </summary>
        /// <param name="id">Sector id to be updaed</param>
        /// <param name="request">Sector info</param>
        /// <returns>Updated sector info</returns>
        [HttpPut("{id}")]
        public virtual async Task<SectorDto> UpdateAsync(string id, [FromBody] SectorRequestModel request)
            => await _sectorService.UpdateAsync(id, request);
        
        /// <summary>
        /// Delete existing sector
        /// </summary>
        /// <param name="id">Sector id to be deleted</param>
        [HttpDelete("{id}")]
        public virtual async Task DeleteAsync(string id)
            => await _sectorService.DeleteAsync(id);
    }
}