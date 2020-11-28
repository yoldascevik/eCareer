using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Data.Pagination;
using Company.Domain.Repository;
using MediatR;

namespace Company.Application.Queries.Company.GetCompanyFollowers
{
    public class GetCompanyFollowersHandler : IRequestHandler<GetCompanyFollowersQuery, PagedList<Guid>>
    {
        private readonly ICompanyFollowerRepository _companyFollowerRepository;

        public GetCompanyFollowersHandler(ICompanyFollowerRepository companyFollowerRepository)
        {
            _companyFollowerRepository = companyFollowerRepository;
        }

        public async Task<PagedList<Guid>> Handle(GetCompanyFollowersQuery request, CancellationToken cancellationToken)
        {
            return await _companyFollowerRepository.GetActiveCompanyFollowers(request.CompanyId)
                .OrderBy(follower => follower.Id)
                .Select(follower => follower.UserId)
                .ToPagedListAsync(request.PaginationFilter);
        }
    }
}