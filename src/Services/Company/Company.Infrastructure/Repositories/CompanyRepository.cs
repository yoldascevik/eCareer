using System;
using System.Linq;
using System.Threading.Tasks;
using Career.EntityFramework.Repositories;
using Company.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

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
                && company.Address.CountryId == countryId
                && (companyId == default || company.Id != companyId));
        }

        public async Task<Domain.Entities.Company> GetCompanyIncludeFollowers(Guid companyId)
        {
            return await Get()
                .Include(x => x.Followers.Where(s => !s.IsDeleted))
                .FirstOrDefaultAsync(x => x.Id == companyId);
        }
    }
}