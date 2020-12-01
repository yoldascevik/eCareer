using System;
using System.Threading.Tasks;
using Career.Repositories.Repository;

namespace Company.Domain.Repositories
{
    public interface ICompanyRepository: IRepository<Entities.Company>
    {
        Task<bool> IsTaxNumberExistsAsync(string taxNumber, string countryId, Guid companyId = default);

        Task<Entities.Company> GetCompanyIncludeFollowers(Guid companyId);
    }
}