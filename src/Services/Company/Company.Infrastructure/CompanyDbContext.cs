using Career.Domain.DomainEvent.Dispatcher;
using Career.EntityFramework;
using Company.Domain.Entities;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}