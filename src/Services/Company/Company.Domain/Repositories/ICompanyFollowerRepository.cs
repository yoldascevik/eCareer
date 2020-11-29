using System;
using System.Linq;
using Career.Repositories.Repository;
using Company.Domain.Entities;

namespace Company.Domain.Repositories
{
    public interface ICompanyFollowerRepository: IRepository<CompanyFollower>
    {
        public IQueryable<CompanyFollower> GetActiveCompanyFollowers(Guid companyId);
        public IQueryable<CompanyFollower> GetFollowedCompaniesOfUser(Guid userId);
    }
}