using Career.EntityFramework;
using Job.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Job.Infrastructure
{
    public class JobDbContext: CareerDbContext
    {
        public JobDbContext() { }
        
        public JobDbContext(DbContextOptions options, IMediator mediator) 
            : base(options, mediator)
        {
            
        }
        
        public DbSet<JobAdvert> JobAdverts { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobEducationLevel> JobEducationLevels { get; set; }
        public DbSet<JobLocation> JobLocations { get; set; }
        public DbSet<JobViewingHistory> JobViewingHistories { get; set; }
        public DbSet<JobWorkType> JobWorkTypes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseNpgsql();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}