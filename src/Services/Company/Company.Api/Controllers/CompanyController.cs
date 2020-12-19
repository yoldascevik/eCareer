using System;
using System.Threading.Tasks;
using Career.Data.Pagination;
using Company.Api.Controllers.Base;
using Company.Application.Company.Commands.CreateCompany;
using Company.Application.Company.Commands.DeleteCompany;
using Company.Application.Company.Commands.UpdateCompanyAddress;
using Company.Application.Company.Commands.UpdateCompanyDetails;
using Company.Application.Company.Commands.UpdateCompanyEmail;
using Company.Application.Company.Commands.UpdateCompanyName;
using Company.Application.Company.Commands.UpdateCompanyTaxInfo;
using Company.Application.Company.Dtos;
using Company.Application.Company.Queries.GetCompanies;
using Company.Application.Company.Queries.GetCompanyAddress;
using Company.Application.Company.Queries.GetCompanyById;
using Company.Application.Company.Queries.GetCompanyDetails;
using Company.Application.Company.Queries.GetCompanyFollowers;
using Company.Application.Company.Queries.GetCompanyTaxInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Company.Api.Controllers
{
    [Route("api/v{version:apiVersion}/companies")]
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
        /// Get company address
        /// </summary>
        /// <param name="id">Company id</param>
        /// <returns>Company address info</returns>
        [HttpGet("{id}/address")]
        public async Task<IActionResult> GetCompanyAddress(Guid id)
            => Ok(await _mediator.Send(new GetCompanyAddressQuery(id)));
        
        /// <summary>
        /// Get company details
        /// </summary>
        /// <param name="id">Company id</param>
        /// <returns>Company details info</returns>
        [HttpGet("{id}/detail")]
        public async Task<IActionResult> GetCompanyDetails(Guid id)
            => Ok(await _mediator.Send(new GetCompanyDetailsQuery(id)));

        /// <summary>
        /// Get company tax info
        /// </summary>
        /// <param name="id">Company id</param>
        /// <returns>Company tax info</returns>
        [HttpGet("{id}/tax")]
        public async Task<IActionResult> GetCompanyTaxInfo(Guid id)
            => Ok(await _mediator.Send(new GetCompanyTaxInfoQuery(id)));
        
        /// <summary>
        /// Create new company
        /// </summary>
        /// <param name="request">Company info</param>
        /// <returns>Created company url</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCompanyCommand request)
        {
            Guid companyId = await _mediator.Send(request);
            return CreatedAtAction(nameof(Get), new {id = companyId});
        }
        
        /// <summary>
        /// Update company tax info
        /// </summary>
        /// <param name="id">Company id to be updaed</param>
        /// <param name="taxInfo">Tax info</param>
        /// <returns>Updated company tax info</returns>
        [HttpPut("{id}/tax")]
        public async Task<IActionResult> UpdateTaxInfoAsync(Guid id, [FromBody] TaxDto taxInfo)
            => Ok(await _mediator.Send(new UpdateCompanyTaxInfoCommand(id, taxInfo)));


        /// <summary>
        /// Update company address
        /// </summary>
        /// <param name="id">Company id to be updaed</param>
        /// <param name="address">Address info</param>
        /// <returns>Updated address info</returns>
        [HttpPut("{id}/address")]
        public async Task<IActionResult> UpdateAddressAsync(Guid id, [FromBody] AddressDto address)
            => Ok(await _mediator.Send(new UpdateCompanyAddressCommand(id, address)));

        /// <summary>
        /// Update company details
        /// </summary>
        /// <param name="id">Company id to be updaed</param>
        /// <param name="detail">Company info</param>
        /// <returns>Updated company detail info</returns>
        [HttpPut("{id}/detail")]
        public async Task<IActionResult> UpdateDetailAsync(Guid id, [FromBody] CompanyDetailDto detail)
            => Ok(await _mediator.Send(new UpdateCompanyDetailsCommand(id, detail)));

        /// <summary>
        /// Update company email address
        /// </summary>
        /// <param name="id">Company id to be updaed</param>
        /// <param name="emailAddress">New email address</param>
        [HttpPut("{id}/email/{emailAddress}")]
        public async Task<IActionResult> UpdateEmailAddressAsync(Guid id, [FromBody] string emailAddress)
            => Ok(await _mediator.Send(new UpdateCompanyEmailCommand(id, emailAddress)));

        /// <summary>
        /// Update company name
        /// </summary>
        /// <param name="id">Company id to be updaed</param>
        /// <param name="companyName">New company name</param>
        [HttpPut("{id}/name/{companyName}")]
        public async Task<IActionResult> UpdateCompanyNameAsync(Guid id, [FromBody] string companyName)
            => Ok(await _mediator.Send(new UpdateCompanyNameCommand(id, companyName)));
        
        /// <summary>
        /// Delete existing company
        /// </summary>
        /// <param name="id">Company id to be deleted</param>
        [HttpDelete("{id}")]
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
    }
}