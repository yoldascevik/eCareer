using Career.Data.Pagination;
using Definition.Api.Constants;
using Definition.Api.Controllers.Base;
using Definition.Application.Work.WorkType;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Definition.Api.Controllers;

[Authorize]
[Route("api/work/types")]
public class WorkTypeController : DefinitionApiController
{
    private readonly IWorkTypeService _workTypeService;

    public WorkTypeController(IWorkTypeService workTypeService)
    {
        _workTypeService = workTypeService;
    }
        
    /// <summary>
    /// Get all work types
    /// </summary>
    [HttpGet]
    public virtual async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter)
        => Ok(await _workTypeService.GetAsync(paginationFilter));
        
    /// <summary>
    /// Get specific work type by id
    /// </summary>
    /// <param name="id">Work type id</param>
    [HttpGet("{id}")]
    public virtual async Task<IActionResult> Get(string id)
        => Ok(await _workTypeService.GetByIdAsync(id));
        
    /// <summary>
    /// Create new work type
    /// </summary>
    /// <param name="request">Work type info</param>
    /// <returns>Created work type info</returns>
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.Manage)]
    public virtual async Task<WorkTypeDto> CreateAsync([FromBody] WorkTypeRequestModel request)
        => await _workTypeService.CreateAsync(request);
        
    /// <summary>
    /// Update existing work type
    /// </summary>
    /// <param name="id">Work type id to be updaed</param>
    /// <param name="request">Work type info</param>
    /// <returns>Updated work type info</returns>
    [HttpPut("{id}")]
    [Authorize(Policy = AuthorizationPolicies.Manage)]
    public virtual async Task<WorkTypeDto> UpdateAsync(string id, [FromBody] WorkTypeRequestModel request)
        => await _workTypeService.UpdateAsync(id, request);
        
    /// <summary>
    /// Delete existing work type
    /// </summary>
    /// <param name="id">Work type id to be deleted</param>
    [HttpDelete("{id}")]
    [Authorize(Policy = AuthorizationPolicies.Manage)]
    public virtual async Task DeleteAsync(string id)
        => await _workTypeService.DeleteAsync(id);
}