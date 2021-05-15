using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Career.Domain.Audit;
using Career.Domain.Entities;
using Career.EventHub;
using Career.Exceptions;
using Career.Shared.Timing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Career.EntityFramework
{
    public class CareerDbContext: DbContext
    {
        private readonly IEventDispatcher _domainEventDispatcher;
        
        protected CareerDbContext()
        {
        }
        
        protected CareerDbContext(DbContextOptions options, IEventDispatcher domainEventDispatcher) : base(options)
        {
            Check.NotNull(options, nameof(options));
            
            _domainEventDispatcher = domainEventDispatcher;
        }

        /// <summary>
        /// Set audit info automatically.
        /// For create: IHasCreationTime, ICreationAudited
        /// For update: IHasModificationTime, IModificationAudited
        /// </summary>
        public bool SetAuditInfoAutomatically { get; set; } = true;

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
            if (!SetAuditInfoAutomatically)
                return;

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
            if (_domainEventDispatcher == null)
                return;
            
            IEnumerable<DomainEntity> domainEntities = ChangeTracker.Entries()
                .Where(e => e.Entity is DomainEntity)
                .Select(e=> e.Entity)
                .Cast<DomainEntity>();

            foreach (DomainEntity domainEntity in domainEntities)
            {
                if (domainEntity.DomainEvents != null && domainEntity.DomainEvents.Any())
                {
                    await _domainEventDispatcher.Dispatch(domainEntity.DomainEvents);
                    domainEntity.ClearDomainEvents();
                }
            }
        }

        #endregion
    }
}