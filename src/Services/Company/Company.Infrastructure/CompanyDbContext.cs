using Microsoft.EntityFrameworkCore;

namespace Company.Infrastructure
{
    public class CompanyDbContext: DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<Domain.Company> Companies { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseNpgsql();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}