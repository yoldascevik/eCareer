using System;
using System.Threading.Tasks;
using Career.Data.Pagination;
using Company.Api.Controllers.Base;
using Company.Application.Queries.CompanyFollower.GetFollowedCompanies;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Company.Api.Controllers
{
    [Route("api/v{version:apiVersion}/company-followers")]
    public class CompanyFollowerController: CompanyApiController
    {
        private readonly IMediator _mediator;
        
        public CompanyFollowerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get followed companies of user.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="paginationFilter">Paging configuration</param>
        [HttpGet("{userId}/companies")]
        public async Task<IActionResult> Get(Guid userId, [FromQuery] PaginationFilter paginationFilter)
            => Ok(await _mediator.Send(new GetFollowedCompaniesQuery(userId, paginationFilter)));

    }
}