using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Data.Pagination;
using Career.MediatR.Query;
using Company.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Company.Application.CompanyFollower.Queries.GetFollowedCompanies
{
    public class GetFollowedCompaniesHandler: IQueryHandler<GetFollowedCompaniesQuery, PagedList<Guid>>
    {
        private readonly ICompanyFollowerRepository _companyFollowerRepository;

        public GetFollowedCompaniesHandler(ICompanyFollowerRepository companyFollowerRepository)
        {
            _companyFollowerRepository = companyFollowerRepository;
        }

        public async Task<PagedList<Guid>> Handle(GetFollowedCompaniesQuery request, CancellationToken cancellationToken)
        {
            return await _companyFollowerRepository.GetFollowedCompaniesOfUser(request.UserId)
                .AsNoTracking()
                .OrderBy(follower => follower.Id)
                .Select(follower => follower.CompanyId)
                .ToPagedListAsync(request.PaginationFilter);
        }
    }
}