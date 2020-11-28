using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Data.Pagination;
using Company.Domain.Repository;
using MediatR;

namespace Company.Application.Queries.CompanyFollower.GetFollowedCompanies
{
    public class GetFollowedCompaniesHandler: IRequestHandler<GetFollowedCompaniesQuery, PagedList<Guid>>
    {
        private readonly ICompanyFollowerRepository _companyFollowerRepository;

        public GetFollowedCompaniesHandler(ICompanyFollowerRepository companyFollowerRepository)
        {
            _companyFollowerRepository = companyFollowerRepository;
        }

        public async Task<PagedList<Guid>> Handle(GetFollowedCompaniesQuery request, CancellationToken cancellationToken)
        {
            return await _companyFollowerRepository.GetFollowedCompaniesOfUser(request.UserId)
                .OrderBy(follower => follower.Id)
                .Select(follower => follower.CompanyId)
                .ToPagedListAsync(request.PaginationFilter);
        }
    }
}