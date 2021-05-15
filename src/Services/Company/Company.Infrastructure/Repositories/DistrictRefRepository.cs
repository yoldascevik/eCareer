using Career.EntityFramework.Repositories;
using Company.Domain.Refs;
using Company.Domain.Repositories;

namespace Company.Infrastructure.Repositories
{
    public class DistrictRefRepository: EfRepository<CompanyDbContext, DistrictRef>, IDistrictRefRepository
    {
        public DistrictRefRepository(CompanyDbContext dbContext) : base(dbContext)
        {
        }
    }
}