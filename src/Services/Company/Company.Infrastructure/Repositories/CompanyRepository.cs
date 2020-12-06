using System;
using System.Threading.Tasks;
using Career.EntityFramework.Repositories;
using Company.Domain.Repositories;

namespace Company.Infrastructure.Repositories
{
    public class CompanyRepository : EfRepository<CompanyDbContext, Domain.Entities.Company>, ICompanyRepository
    {
        public CompanyRepository(CompanyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsTaxNumberExistsAsync(string taxNumber, string countryId, Guid companyId = default)
        {
            return await AnyAsync(company =>
                company.TaxInfo.TaxNumber == taxNumber
                && company.IsDeleted == false
                && company.AddressInfo.CountryId == countryId
                && (companyId == default || company.Id != companyId));
        }

        public async Task<Domain.Entities.Company> GetCompanyByIdAsync(Guid companyId)
        {
            return await FirstOrDefaultAsync(c => c.Id == companyId && !c.IsDeleted);
        }
    }
}