using System;
using Career.Exceptions;
using Job.Domain.TagAggregate;

namespace Job.Domain.JobAggregate
{
    public class TagRef
    {
        private TagRef() { }
        
        public Guid Id { get; private init; }
        public string Name { get; private set; }

        public static TagRef CreateFromTag(Tag tag)
        {
            Check.NotNull(tag, nameof(tag));
            return new TagRef()
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public TagRef SetName(string name)
        {
            Check.NotNullOrEmpty(name, nameof(name));
            Name = name;

            return this;
        }
    }
}