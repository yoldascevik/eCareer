using Career.EntityFramework.Repositories;
using Company.Domain.Refs;
using Company.Domain.Repositories;

namespace Company.Infrastructure.Repositories
{
    public class CityRefRepository: EfRepository<CompanyDbContext, CityRef>, ICityRefRepository
    {
        public CityRefRepository(CompanyDbContext dbContext) : base(dbContext)
        {
        }
    }
}