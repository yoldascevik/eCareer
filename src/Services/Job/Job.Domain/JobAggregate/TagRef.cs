using System;
using Career.Exceptions;
using Job.Domain.TagAggregate;

namespace Job.Domain.JobAggregate
{
    public class TagRef
    {
        private TagRef() { }
        
        public Guid TagId { get; private set; }
        public string Name { get; private set; }

        public static TagRef CreateFromTag(Tag tag)
        {
            Check.NotNull(tag, nameof(tag));
            return new TagRef()
            {
                TagId = tag.Id,
                Name = tag.Name
            };
        }
    }
}