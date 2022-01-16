using Career.Data.Pagination;
using Definition.Api.Constants;
using Definition.Api.Controllers.Base;
using Definition.Application.Location.City;
using Definition.Application.Location.Country;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Definition.Api.Controllers;

[Authorize]
[Route("api/locations/countries")]
public class CountryController : DefinitionApiController
{
    private readonly ICountryService _countryService;
    private readonly ICityService _cityService;

    public CountryController(ICountryService countryService, ICityService cityService)
    {
        _countryService = countryService;
        _cityService = cityService;
    }
        
    /// <summary>
    /// Get all countries
    /// </summary>
    [HttpGet]
    public virtual async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter)
        => Ok(await _countryService.GetAsync(paginationFilter));

    /// <summary>
    /// Get cities of country
    /// </summary>
    /// <param name="id">Country id</param>
    /// <param name="paginationFilter">Pagination configuration</param>
    [HttpGet("{id}/cities")]
    public virtual async Task<IActionResult> Get(string id, [FromQuery] PaginationFilter paginationFilter)
        => Ok(await _cityService.GetByCountryId(id, paginationFilter));
        
    /// <summary>
    /// Get specific country by id
    /// </summary>
    /// <param name="id">Country id</param>
    [HttpGet("{id}")]
    public virtual async Task<IActionResult> Get(string id)
        => Ok(await _countryService.GetByIdAsync(id));

    /// <summary>
    /// Get specific country by code
    /// </summary>
    /// <param name="code">Country code</param>
    [HttpGet("code/{code}")]
    public virtual async Task<IActionResult> GetByCode(string code)
        => Ok(await _countryService.GetByCodeAsync(code));
        
    /// <summary>
    /// Create new Country
    /// </summary>
    /// <param name="request">Country info</param>
    /// <returns>Created country info</returns>
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.Manage)]
    public virtual async Task<CountryDto> CreateAsync([FromBody] CountryRequestModel request)
        => await _countryService.CreateAsync(request);
        
    /// <summary>
    /// Update existing country
    /// </summary>
    /// <param name="id">Country id to be updaed</param>
    /// <param name="request">Country info</param>
    /// <returns>Updated country info</returns>
    [HttpPut("{id}")]
    [Authorize(Policy = AuthorizationPolicies.Manage)]
    public virtual async Task<CountryDto> UpdateAsync(string id, [FromBody] CountryRequestModel request)
        => await _countryService.UpdateAsync(id, request);
        
    /// <summary>
    /// Delete existing country
    /// </summary>
    /// <param name="id">Country id to be deleted</param>
    [HttpDelete("{id}")]
    [Authorize(Policy = AuthorizationPolicies.Manage)]
    public virtual async Task DeleteAsync(string id)
        => await _countryService.DeleteAsync(id);
}