using System;
using System.Threading.Tasks;
using Career.EntityFramework.Repositories;
using Company.Domain.Repository;

namespace Company.Infrastructure.Repositories
{
    public class CompanyRepository: EfRepository<CompanyDbContext, Domain.Entities.Company>, ICompanyRepository
    {
        public CompanyRepository(CompanyDbContext dbContext) : base(dbContext) { }

        public async Task<bool> IsTaxNumberExistsAsync(string taxNumber, string countryId, Guid companyId = default)
        {
            return await AnyAsync(company =>
                company.TaxNumber == taxNumber
                && company.IsDeleted == false
                && company.CountryId == countryId
                && (companyId == default || company.Id != companyId));
        }
    }
}