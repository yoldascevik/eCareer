using Career.EntityFramework;
using Company.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Company.Infrastructure
{
    public class CompanyDbContext: CareerDbContext
    {
        public CompanyDbContext() { }
        
        public CompanyDbContext(DbContextOptions options, IMediator mediator) 
            : base(options, mediator)
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