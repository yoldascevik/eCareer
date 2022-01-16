using Career.EntityFramework.Repositories;
using Company.Domain.Refs;
using Company.Domain.Repositories;

namespace Company.Infrastructure.Repositories;

public class CountryRefRepository: EfRepository<CompanyDbContext, CountryRef>, ICountryRefRepository
{
    public CountryRefRepository(CompanyDbContext dbContext) : base(dbContext)
    {
    }
}