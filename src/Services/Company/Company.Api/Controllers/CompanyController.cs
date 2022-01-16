using Career.Data.Pagination;
using Company.Api.Constants;
using Company.Api.Controllers.Base;
using Company.Application.Company.Commands.AddNewAddress;
using Company.Application.Company.Commands.CreateCompany;
using Company.Application.Company.Commands.DeleteAddress;
using Company.Application.Company.Commands.DeleteCompany;
using Company.Application.Company.Commands.UpdateAddress;
using Company.Application.Company.Commands.UpdateCompanyDetails;
using Company.Application.Company.Commands.UpdateCompanyEmail;
using Company.Application.Company.Commands.UpdateCompanyName;
using Company.Application.Company.Commands.UpdateCompanyTaxInfo;
using Company.Application.Company.Dtos;
using Company.Application.Company.Queries.GetCompanies;
using Company.Application.Company.Queries.GetCompanyAddressById;
using Company.Application.Company.Queries.GetCompanyAddresses;
using Company.Application.Company.Queries.GetCompanyById;
using Company.Application.Company.Queries.GetCompanyFollowers;
using Company.Application.Company.Queries.GetCompanyTaxInfo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Api.Controllers;

[Route("api/companies")]
public class CompanyController : CompanyApiController
{
    private readonly IMediator _mediator;

    public CompanyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all companies
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetCompaniesQuery request)
        => Ok(await _mediator.Send(request));

    /// <summary>
    /// Get specific company by id
    /// </summary>
    /// <param name="id">Company id</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
        => Ok(await _mediator.Send(new GetCompanyByIdQuery(id)));

    /// <summary>
    /// Get company tax info
    /// </summary>
    /// <param name="id">Company id</param>
    /// <returns>Company tax info</returns>
    [HttpGet("{id}/tax")]
    public async Task<IActionResult> GetCompanyTaxInfo(Guid id)
        => Ok(await _mediator.Send(new GetCompanyTaxInfoQuery(id)));

    /// <summary>
    /// Update company tax info
    /// </summary>
    /// <param name="id">Company id to be updaed</param>
    /// <param name="taxInfo">Tax info</param>
    /// <returns>Updated company tax info</returns>
    [HttpPut("{id}/tax")]
    [Authorize(Policy = AuthorizationPolicies.ManageCompany)]
    public async Task<IActionResult> UpdateTaxInfoAsync(Guid id, [FromBody] TaxDto taxInfo)
        => Ok(await _mediator.Send(new UpdateCompanyTaxInfoCommand(id, taxInfo)));

    /// <summary>
    /// Create new company
    /// </summary>
    /// <param name="request">Company info</param>
    /// <returns>Created company url</returns>
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.ManageCompany)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCompanyCommand request)
    {
        Guid companyId = await _mediator.Send(request);
        return CreatedAtAction(nameof(Get), new {id = companyId}, null);
    }

    /// <summary>
    /// Update company details
    /// </summary>
    /// <param name="id">Company id to be updaed</param>
    /// <param name="detail">Company info</param>
    /// <returns>Updated company detail info</returns>
    [HttpPut("{id}/detail")]
    [Authorize(Policy = AuthorizationPolicies.ManageCompany)]
    public async Task<IActionResult> UpdateDetailAsync(Guid id, [FromBody] CompanyDetailDto detail)
        => Ok(await _mediator.Send(new UpdateCompanyDetailsCommand(id, detail)));

    /// <summary>
    /// Update company email address
    /// </summary>
    /// <param name="id">Company id to be updaed</param>
    /// <param name="emailAddress">New email address</param>
    [HttpPut("{id}/email/{emailAddress}")]
    [Authorize(Policy = AuthorizationPolicies.ManageCompany)]
    public async Task<IActionResult> UpdateEmailAddressAsync(Guid id, [FromRoute] string emailAddress)
        => Ok(await _mediator.Send(new UpdateCompanyEmailCommand(id, emailAddress)));

    /// <summary>
    /// Update company name
    /// </summary>
    /// <param name="id">Company id to be updaed</param>
    /// <param name="companyName">New company name</param>
    [HttpPut("{id}/name/{companyName}")]
    [Authorize(Policy = AuthorizationPolicies.ManageCompany)]
    public async Task<IActionResult> UpdateCompanyNameAsync(Guid id, [FromRoute] string companyName)
        => Ok(await _mediator.Send(new UpdateCompanyNameCommand(id, companyName)));

    /// <summary>
    /// Delete existing company
    /// </summary>
    /// <param name="id">Company id to be deleted</param>
    [HttpDelete("{id}")]
    [Authorize(Policy = AuthorizationPolicies.ManageCompany)]
    public async Task DeleteAsync(Guid id)
        => Ok(await _mediator.Send(new DeleteCompanyCommand(id)));

    /// <summary>
    /// Get company followers
    /// </summary>
    /// <param name="id">Company id</param>
    /// <param name="paginationFilter">Paging configuration</param>
    /// <returns></returns>
    [HttpGet("{id}/followers")]
    public async Task<IActionResult> GetCompanyFollowers(Guid id, [FromQuery] PaginationFilter paginationFilter)
        => Ok(await _mediator.Send(new GetCompanyFollowersQuery(id, paginationFilter)));

    /// <summary>
    /// Get company addresses
    /// </summary>
    /// <param name="id">Company id</param>
    /// <param name="paginationFilter">Paging configuration</param>
    /// <returns>Company address info</returns>
    [HttpGet("{id}/addresses")]
    public async Task<IActionResult> GetAddresses(Guid id, [FromQuery] PaginationFilter paginationFilter)
        => Ok(await _mediator.Send(new GetCompanyAddressesQuery(id, paginationFilter)));

    /// <summary>
    /// Get company address by id
    /// </summary>
    /// <param name="id">Company id</param>
    /// <param name="addressId">Address id</param>
    /// <returns>Company address info</returns>
    [HttpGet("{id}/addresses/{addressId}")]
    public async Task<IActionResult> GetAddressById(Guid id, Guid addressId)
        => Ok(await _mediator.Send(new GetCompanyAddressesByIdQuery(id, addressId)));

    /// <summary>
    /// Add new company address
    /// </summary>
    /// <param name="id">Company id</param>
    /// <param name="address">Address info</param>
    /// <returns>Company address info</returns>
    [HttpPost("{id}/addresses")]
    [Authorize(Policy = AuthorizationPolicies.ManageCompany)]
    public async Task<IActionResult> AddAddress(Guid id, [FromBody] AddressInputDto address)
        => Ok(await _mediator.Send(new AddNewAddressCommand(id, address)));

    /// <summary>
    /// Update company address
    /// </summary>
    /// <param name="id">Company id</param>
    /// <param name="addressId">Address id</param>
    /// <param name="address">Address info</param>
    /// <returns>Company address info</returns>
    [HttpPut("{id}/addresses/{addressId}")]
    [Authorize(Policy = AuthorizationPolicies.ManageCompany)]
    public async Task<IActionResult> UpdateAddress(Guid id, Guid addressId, [FromBody] AddressInputDto address)
        => Ok(await _mediator.Send(new UpdateAddressCommand(id, addressId, address)));
        
    /// <summary>
    /// Delete company address
    /// </summary>
    /// <param name="id">Company id</param>
    /// <param name="addressId">Address id</param>
    [HttpDelete("{id}/addresses/{addressId}")]
    [Authorize(Policy = AuthorizationPolicies.ManageCompany)]
    public async Task<IActionResult> DeleteAddress(Guid id, Guid addressId)
        => Ok(await _mediator.Send(new DeleteAddressCommand(id, addressId)));
}