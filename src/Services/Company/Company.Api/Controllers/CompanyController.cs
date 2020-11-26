using System;
using System.Threading.Tasks;
using Career.Data.Pagination;
using Company.Api.Controllers.Base;
using Company.Application.Commands.Company.CreateCompany;
using Company.Application.Commands.Company.DeleteCompany;
using Company.Application.Commands.Company.UpdateCompany;
using Company.Application.Dtos.Company;
using Company.Application.Queries.Company.GetCompanies;
using Company.Application.Queries.Company.GetCompanyById;
using Company.Application.Queries.Company.GetCompanyFollowers;
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
        /// Create new company
        /// </summary>
        /// <param name="request">Company info</param>
        /// <returns>Created company url</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCompanyCommand request)
        {
            var createdCompany = await _mediator.Send(request);
            if (createdCompany == null)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new {id = createdCompany.Id});
        }

        /// <summary>
        /// Update existing company
        /// </summary>
        /// <param name="id">Company id to be updaed</param>
        /// <param name="request">Company info</param>
        /// <returns>Updated company info</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CompanyRequest request)
            => Ok(await _mediator.Send(new UpdateCompanyCommmand(id, request)));

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