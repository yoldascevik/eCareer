using Career.EntityFramework.Repositories;
using Company.Domain.Refs;
using Company.Domain.Repositories;

namespace Company.Infrastructure.Repositories;

public class SectorRefRepository: EfRepository<CompanyDbContext, SectorRef>, ISectorRefRepository
{
    public SectorRefRepository(CompanyDbContext dbContext) : base(dbContext)
    {
    }
}