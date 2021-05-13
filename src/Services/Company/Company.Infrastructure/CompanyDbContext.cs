using Career.Domain.DomainEvent.Dispatcher;
using Career.EntityFramework;
using Company.Domain.Entities;
using Company.Domain.Refs;
using Microsoft.EntityFrameworkCore;

namespace Company.Infrastructure
{
    public class CompanyDbContext : CareerDbContext
    {
        public CompanyDbContext()
        {
        }

        public CompanyDbContext(DbContextOptions options, IDomainEventDispatcher domainEventDispatcher)
            : base(options, domainEventDispatcher)
        {
        }

        public DbSet<Domain.Entities.Company> Companies { get; set; }
        public DbSet<CompanyFollower> CompanyFollowers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        
        // Refs
        public DbSet<SectorRef> Sectors { get; set; }
        public DbSet<CountryRef> Countries { get; set; }
        public DbSet<CityRef> Cities { get; set; }
        public DbSet<DistrictRef> Districts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}