using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Shared.Audit;
using Career.Shared.System.DateTimeProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Career.EntityFramework
{
    public class AuditedDbContext: DbContext
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        
        protected AuditedDbContext() { }
        
        protected AuditedDbContext(DbContextOptions options, IDateTimeProvider dateTimeProvider)
            : base(options)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public override int SaveChanges()
        {
            SetAuditData();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetAuditData();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetAuditData();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void SetAuditData()
        {
            IEnumerable<EntityEntry> entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                SetCreationAudits(entityEntry);
                SetModificationAudits(entityEntry);
            }
        }

        private void SetCreationAudits(EntityEntry entityEntry)
        {
            if (entityEntry.State == EntityState.Added)
            {
                if (entityEntry.Entity is ICreationAudited creationAuditedEntity)
                {
                    creationAuditedEntity.CreatorUserId = null; // TODO: set current user id
                    creationAuditedEntity.CreationTime = _dateTimeProvider.UtcNow;
                }
                else if (entityEntry.Entity is IHasCreationTime hasCreationTimeEntity)
                    hasCreationTimeEntity.CreationTime = _dateTimeProvider.UtcNow;
            }
        }

        private void SetModificationAudits(EntityEntry entityEntry)
        {
            if (entityEntry.Entity is IModificationAudited modificationAuditedEntity)
            {
                modificationAuditedEntity.LastModifierUserId = null; // TODO: set current user id
                modificationAuditedEntity.LastModificationTime = _dateTimeProvider.UtcNow;
            }
            else if (entityEntry.Entity is IHasModificationTime hasModificationTimeEntity)
                hasModificationTimeEntity.LastModificationTime = _dateTimeProvider.UtcNow;
        }
    }
}