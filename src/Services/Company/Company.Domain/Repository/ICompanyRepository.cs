using System;
using System.Threading.Tasks;
using Career.Repositories.Repository;

namespace Company.Domain.Repository
{
    public interface ICompanyRepository: IRepository<Entities.Company>
    {
        Task<bool> IsTaxNumberExistsAsync(string taxNumber, string countryId, Guid companyId = default);
    }
}