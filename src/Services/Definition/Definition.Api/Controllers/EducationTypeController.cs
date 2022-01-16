using System.Threading.Tasks;
using Career.Data.Pagination;
using Definition.Api.Constants;
using Definition.Api.Controllers.Base;
using Definition.Application.Education.EducationType;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Definition.Api.Controllers;

[Authorize]
[Route("api/education/types")]
public class EducationTypeController : DefinitionApiController
{
    private readonly IEducationTypeService _educationTypeService;

    public EducationTypeController(IEducationTypeService educationTypeService)
    {
        _educationTypeService = educationTypeService;
    }
        
    /// <summary>
    /// Get all education types
    /// </summary>
    [HttpGet]
    public virtual async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter)
        => Ok(await _educationTypeService.GetAsync(paginationFilter));
        
    /// <summary>
    /// Get specific education type by id
    /// </summary>
    /// <param name="id">Education type id</param>
    [HttpGet("{id}")]
    public virtual async Task<IActionResult> Get(string id)
        => Ok(await _educationTypeService.GetByIdAsync(id));
        
    /// <summary>
    /// Create new education type
    /// </summary>
    /// <param name="request">Education type info</param>
    /// <returns>Created education type info</returns>
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.Manage)]
    public virtual async Task<EducationTypeDto> CreateAsync([FromBody] EducationTypeRequestModel request)
        => await _educationTypeService.CreateAsync(request);
        
    /// <summary>
    /// Update existing education type
    /// </summary>
    /// <param name="id">Education type id to be updaed</param>
    /// <param name="request">Education type info</param>
    /// <returns>Updated education type info</returns>
    [HttpPut("{id}")]
    [Authorize(Policy = AuthorizationPolicies.Manage)]
    public virtual async Task<EducationTypeDto> UpdateAsync(string id, [FromBody] EducationTypeRequestModel request)
        => await _educationTypeService.UpdateAsync(id, request);
        
    /// <summary>
    /// Delete existing education type
    /// </summary>
    /// <param name="id">Education type id to be deleted</param>
    [HttpDelete("{id}")]
    [Authorize(Policy = AuthorizationPolicies.Manage)]
    public virtual async Task DeleteAsync(string id)
        => await _educationTypeService.DeleteAsync(id);
}