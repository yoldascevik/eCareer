using System.Threading.Tasks;
using Career.Data.Pagination;
using Definition.Api.Constants;
using Definition.Api.Controllers.Base;
using Definition.Application.Education.ScholarshipType;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Definition.Api.Controllers;

[Authorize]
[Route("api/education/scholarshiptypes")]
public class ScholarshipTypeController : DefinitionApiController
{
    private readonly IScholarshipTypeService _scholarshipTypeService;

    public ScholarshipTypeController(IScholarshipTypeService scholarshipTypeService)
    {
        _scholarshipTypeService = scholarshipTypeService;
    }
        
    /// <summary>
    /// Get all scholarship types
    /// </summary>
    [HttpGet]
    public virtual async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter)
        => Ok(await _scholarshipTypeService.GetAsync(paginationFilter));
        
    /// <summary>
    /// Get specific scholarship type by id
    /// </summary>
    /// <param name="id">Scholarship type id</param>
    [HttpGet("{id}")]
    public virtual async Task<IActionResult> Get(string id)
        => Ok(await _scholarshipTypeService.GetByIdAsync(id));
        
    /// <summary>
    /// Create new scholarship type
    /// </summary>
    /// <param name="request">Scholarship type info</param>
    /// <returns>Created scholarship type info</returns>
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.Manage)]
    public virtual async Task<ScholarshipTypeDto> CreateAsync([FromBody] ScholarshipTypeRequestModel request)
        => await _scholarshipTypeService.CreateAsync(request);
        
    /// <summary>
    /// Update existing scholarship type
    /// </summary>
    /// <param name="id">Scholarship type id to be updaed</param>
    /// <param name="request">Scholarship type info</param>
    /// <returns>Updated scholarship type info</returns>
    [HttpPut("{id}")]
    [Authorize(Policy = AuthorizationPolicies.Manage)]
    public virtual async Task<ScholarshipTypeDto> UpdateAsync(string id, [FromBody] ScholarshipTypeRequestModel request)
        => await _scholarshipTypeService.UpdateAsync(id, request);
        
    /// <summary>
    /// Delete existing scholarship type
    /// </summary>
    /// <param name="id">Scholarship type id to be deleted</param>
    [HttpDelete("{id}")]
    [Authorize(Policy = AuthorizationPolicies.Manage)]
    public virtual async Task DeleteAsync(string id)
        => await _scholarshipTypeService.DeleteAsync(id);
}