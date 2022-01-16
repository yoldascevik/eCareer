using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Data.Pagination;
using Career.MediatR.Query;
using Company.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Company.Application.Company.Queries.GetCompanyFollowers;

public class GetCompanyFollowersHandler : IQueryHandler<GetCompanyFollowersQuery, PagedList<Guid>>
{
    private readonly ICompanyFollowerRepository _companyFollowerRepository;

    public GetCompanyFollowersHandler(ICompanyFollowerRepository companyFollowerRepository)
    {
        _companyFollowerRepository = companyFollowerRepository;
    }

    public async Task<PagedList<Guid>> Handle(GetCompanyFollowersQuery request, CancellationToken cancellationToken)
    {
        return await _companyFollowerRepository.GetActiveCompanyFollowers(request.CompanyId)
            .AsNoTracking()
            .OrderBy(follower => follower.Id)
            .Select(follower => follower.UserId)
            .ToPagedListAsync(request.PaginationFilter);
    }
}