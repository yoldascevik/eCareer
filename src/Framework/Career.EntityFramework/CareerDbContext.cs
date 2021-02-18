using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Domain.Audit;
using Career.Domain.Entities;
using Career.Shared.Timing;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Career.EntityFramework
{
    public class CareerDbContext: DbContext
    {
        private readonly IMediator _mediator;
        
        protected CareerDbContext()
        {
            
        }
        
        protected CareerDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public override int SaveChanges()
        {
            SetAuditData();
            int result = base.SaveChanges();

            Task.Run(DispatchDomainEventsAsync);

            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetAuditData();
            int result = await base.SaveChangesAsync(cancellationToken);

            await DispatchDomainEventsAsync();

            return result;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetAuditData();
            int result = base.SaveChanges(acceptAllChangesOnSuccess);

            Task.Run(DispatchDomainEventsAsync);

            return result;
        }

        #region Private Helpers Of Audit Data

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
                    creationAuditedEntity.CreationTime = Clock.Now;
                }
                else if (entityEntry.Entity is IHasCreationTime hasCreationTimeEntity)
                    hasCreationTimeEntity.CreationTime = Clock.Now;
            }
        }

        private void SetModificationAudits(EntityEntry entityEntry)
        {
            if (entityEntry.Entity is IModificationAudited modificationAuditedEntity)
            {
                modificationAuditedEntity.LastModifiedUserId = null; // TODO: set current user id
                modificationAuditedEntity.LastModificationTime = Clock.Now;
            }
            else if (entityEntry.Entity is IHasModificationTime hasModificationTimeEntity)
                hasModificationTimeEntity.LastModificationTime = Clock.Now;
        }

        #endregion

        #region Private Helpers Of DomainEvent

        private async Task DispatchDomainEventsAsync()
        {
            IEnumerable<DomainEntity> domainEntities = ChangeTracker.Entries()
                .Where(e => e.Entity is DomainEntity)
                .Select(e=> e.Entity)
                .Cast<DomainEntity>();

            foreach (DomainEntity domainEntity in domainEntities)
            {
                if (domainEntity.DomainEvents != null && domainEntity.DomainEvents.Any())
                {
                    foreach (var domainEvent in domainEntity.DomainEvents)
                    {
                        await _mediator.Publish(domainEvent);
                    }
                    
                    domainEntity.ClearDomainEvents();
                }
            }
        }

        #endregion
    }
}