using System;
using System.Threading.Tasks;
using Career.Data.Pagination;
using Company.Api.Controllers.Base;
using Company.Application.Commands.CompanyFollower.FollowCompany;
using Company.Application.Commands.CompanyFollower.UnfollowCompany;
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

        /// <summary>
        /// Follow company
        /// </summary>
        /// <param name="userId">Follower user id</param>
        /// <param name="companyId">Company id</param>
        /// <returns></returns>
        [HttpPost("{userId}/follow/{companyId}")]
        public async Task<IActionResult> FollowCompany(Guid userId, Guid companyId)
            => Ok(await _mediator.Send(new FollowCompanyCommand(userId, companyId)));
        
        /// <summary>
        /// Unfollow company
        /// </summary>
        /// <param name="userId">Follower user id</param>
        /// <param name="companyId">Company id</param>
        /// <returns></returns>
        [HttpDelete("{userId}/unfollow/{companyId}")]
        public async Task<IActionResult> UnfollowCompany(Guid userId, Guid companyId)
            => Ok(await _mediator.Send(new UnfollowCompanyCommand(userId, companyId)));
    }
}