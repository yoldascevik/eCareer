using System;
using System.Linq;
using Career.EntityFramework.Repositories;
using Company.Domain.Entities;
using Company.Domain.Repository;

namespace Company.Infrastructure.Repositories
{
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
    }
}