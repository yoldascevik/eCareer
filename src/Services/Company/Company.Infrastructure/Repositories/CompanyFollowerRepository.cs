using Career.EntityFramework.Repositories;
using Company.Domain.Entities;
using Company.Domain.Repositories;

namespace Company.Infrastructure.Repositories;

public class CompanyFollowerRepository: EfRepository<CompanyDbContext, CompanyFollower>, ICompanyFollowerRepository
{
    public CompanyFollowerRepository(CompanyDbContext dbContext) : base(dbContext) { }
        
    public IQueryable<CompanyFollower> GetActiveCompanyFollowers(Guid companyId)
    {
        return Get(follower => follower.CompanyId == companyId && !follower.IsDeleted);
    }

    public IQueryable<CompanyFollower> GetFollowedCompaniesOfUser(Guid userId)
    {
        return Get(follower => follower.UserId == userId && !follower.IsDeleted);
    }

    public bool CheckUserExistsInCompanyFollowers(Guid userId, Guid companyId)
    {
        return Any(x => x.UserId == userId && x.CompanyId == companyId && !x.IsDeleted);
    }

    public async Task<CompanyFollower> GetCompanyFollower(Guid companyId, Guid userId)
    {
        return await FirstOrDefaultAsync(x => x.CompanyId == companyId && x.UserId == userId && !x.IsDeleted);
    }
}