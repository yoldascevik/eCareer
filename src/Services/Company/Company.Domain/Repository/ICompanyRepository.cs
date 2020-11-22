using System;
using Career.Repositories.Repository;

namespace Company.Domain.Repository
{
    public interface ICompanyRepository: IRepository<Company>
    {
        public bool IsTaxNumberExists(string taxNumber, string countryId, Guid companyId = default);
    }
}