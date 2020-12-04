using Career.EntityFramework;
using Career.Shared.System.DateTimeProvider;
using Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company.Infrastructure
{
    public class CompanyDbContext: AuditedDbContext
    {
        public CompanyDbContext() { }
        
        public CompanyDbContext(DbContextOptions options, IDateTimeProvider dateTimeProvider)
            : base(options, dateTimeProvider)
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