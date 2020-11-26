using System;
using System.Linq;
using Career.Repositories.Repository;
using Company.Domain.Entities;

namespace Company.Domain.Repository
{
    public interface ICompanyFollowerRepository: IRepository<CompanyFollower>
    {
        public IQueryable<CompanyFollower> GetActiveCompanyFollowers(Guid companyId);
    }
}