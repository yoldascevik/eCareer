using System;
using Career.Domain;
using Career.Domain.Entities;
using Career.Shared.Timing;
using Job.Domain.TagAggregate.Events;

namespace Job.Domain.TagAggregate
{
    public class Tag : DomainEntity, IAggregateRoot
    {
        private Tag() { }

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public bool IsDeleted { get; private set; }
        public DateTime? LastModificationTime { get; private set; }
        public Guid? LastModifiedUserId { get; private set; }

        public static Tag Create(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            return new Tag
            {
                Id = Guid.NewGuid(),
                Name = name.ToLowerInvariant(),
                LastModificationTime = Clock.Now,
                LastModifiedUserId = Guid.Empty //Todo
            };
        }

        public Tag SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            string oldTagName = Name;
            Name = name.ToLowerInvariant();
            LastModificationTime = Clock.Now;
            LastModifiedUserId = Guid.Empty; //Todo

            AddDomainEvent(new TagNameChangedEvent(this, oldTagName));
            return this;
        }

        public void MarkAsDelete()
        {
            if (!IsDeleted)
            {
                IsDeleted = true;
                LastModificationTime = Clock.Now;
                LastModifiedUserId = Guid.Empty; //Todo
                AddDomainEvent(new TagDeletedEvent(this));
            }
        }
    }
}