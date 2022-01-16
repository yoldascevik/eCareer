using System.Threading.Tasks;
using Career.Data.Pagination;
using Definition.Api.Constants;
using Definition.Api.Controllers.Base;
using Definition.Application.Location.City;
using Definition.Application.Location.District;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Definition.Api.Controllers;

[Authorize]
[Route("api/locations/cities")]
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
    [Authorize(Policy = AuthorizationPolicies.Manage)]
    public virtual async Task<CityDto> CreateAsync([FromBody] CityRequestModel request)
        => await _cityService.CreateAsync(request);

    /// <summary>
    /// Update existing city
    /// </summary>
    /// <param name="id">City id to be updaed</param>
    /// <param name="request">City info</param>
    /// <returns>Updated city info</returns>
    [HttpPut("{id}")]
    [Authorize(Policy = AuthorizationPolicies.Manage)]
    public virtual async Task<CityDto> UpdateAsync(string id, [FromBody] CityRequestModel request)
        => await _cityService.UpdateAsync(id, request);

    /// <summary>
    /// Delete existing city
    /// </summary>
    /// <param name="id">City id to be deleted</param>
    [HttpDelete("{id}")]
    [Authorize(Policy = AuthorizationPolicies.Manage)]
    public virtual async Task DeleteAsync(string id)
        => await _cityService.DeleteAsync(id);
}