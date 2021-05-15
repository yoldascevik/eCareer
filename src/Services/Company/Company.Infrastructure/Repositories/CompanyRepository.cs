using System;
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
                && company.TaxInfo.TaxCountryId == countryId
                && company.Id != companyId);
        }

        public bool IsCompanyEmailExists(string email, Guid companyId = default)
        {
            return Any(c => c.Email == email && c.Id != companyId);
        }

        public async Task<Domain.Entities.Company> GetCompanyByIdAsync(Guid companyId)
        {
            return await Get(c => c.Id == companyId && !c.IsDeleted)
                .Include(p=> p.Sector)
                .Include(p=> p.Addresses)
                    .ThenInclude(address => address.CountryRef)
                .Include(p=> p.Addresses)
                    .ThenInclude(address => address.CityRef)
                .Include(p=> p.Addresses)
                    .ThenInclude(address => address.DistrictRef)
                .FirstOrDefaultAsync();
        }
    }
}