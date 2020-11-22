using System;
using System.Threading.Tasks;
using Company.Api.Controllers.Base;
using Company.Application;
using Company.Application.Commands.CreateCompany;
using Company.Application.Commands.UpdateCompany;
using Company.Application.Queries.GetCompanies;
using Company.Application.Queries.GetCompanyById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Company.Api.Controllers
{
    [Route("api/v{version:apiVersion}/companies")]
    public class CompanyController: CompanyApiController
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
        public virtual async Task<IActionResult> Get([FromQuery] GetCompaniesQuery request)
            => Ok(await _mediator.Send(request));

        /// <summary>
        /// Get specific company by id
        /// </summary>
        /// <param name="id">Company id</param>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(Guid id)
            => Ok(await _mediator.Send(new GetCompanyByIdQuery(id)));

        /// <summary>
        /// Create new company
        /// </summary>
        /// <param name="request">Company info</param>
        /// <returns>Created company url</returns>
        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync([FromBody] CreateCompanyCommand request)
        {
            var createdCompany = await _mediator.Send(request);
            if (createdCompany == null)
                return BadRequest();
                
            return CreatedAtAction("Get", createdCompany.Id);
        }

        /// <summary>
        /// Update existing company
        /// </summary>
        /// <param name="id">Company id to be updaed</param>
        /// <param name="request">Company info</param>
        /// <returns>Updated company info</returns>
        [HttpPut("{id}")]
        public virtual async Task<CompanyDto> UpdateAsync(Guid id, [FromBody] UpdateCompanyCommand request)
            => Ok(await _mediator.Send(request));

        //
        // /// <summary>
        // /// Delete existing company
        // /// </summary>
        // /// <param name="id">Company id to be deleted</param>
        // [HttpDelete("{id}")]
        // public virtual async Task DeleteAsync(Guid id)
        //     => throw new NotImplementedException(); // ok
    }
}