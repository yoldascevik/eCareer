﻿using System.Threading.Tasks;
using Career.Utilities.Pagination;
using Definition.Api.Controllers.Base;
using Definition.Application.Work.Sector;
using Microsoft.AspNetCore.Mvc;

namespace Definition.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/work/sector")]
    public class SectorController : DefinitionApiController
    {
        private readonly ISectorService _sectorService;

        public SectorController(ISectorService sectorService)
        {
            _sectorService = sectorService;
        }
        
        /// <summary>
        /// Get all sectors
        /// </summary>
        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter)
            => Ok(await _sectorService.GetAsync(paginationFilter));
        
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