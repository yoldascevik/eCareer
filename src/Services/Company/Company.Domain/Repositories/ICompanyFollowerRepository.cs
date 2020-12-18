using System;
using System.Linq;
using System.Threading.Tasks;
using Career.Repositories.Repository;
using Company.Domain.Entities;

namespace Company.Domain.Repositories
{
    public interface ICompanyFollowerRepository: IRepository<CompanyFollower>
    {
        IQueryable<CompanyFollower> GetActiveCompanyFollowers(Guid companyId);
        IQueryable<CompanyFollower> GetFollowedCompaniesOfUser(Guid userId);
        bool CheckUserExistsInCompanyFollowers(Guid companyId, Guid userId);
        Task<CompanyFollower> GetCompanyFollower(Guid companyId, Guid userId);
    }
}