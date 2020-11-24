using System;
using System.Threading.Tasks;
using Career.EntityFramework.Repositories;
using Company.Domain.Repository;

namespace Company.Infrastructure.Repositories
{
    public class CompanyRepository: EfRepository<CompanyDbContext, Domain.Company>, ICompanyRepository
    {
        public CompanyRepository(CompanyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsTaxNumberExistsAsync(string taxNumber, string countryId, Guid companyId = default)
        {
            throw new NotImplementedException();
        }
    }
}