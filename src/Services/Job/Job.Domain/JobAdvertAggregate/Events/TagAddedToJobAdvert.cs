using Career.Domain.DomainEvent;
using Career.Exceptions;
using Job.Domain.TagAggregate;

namespace Job.Domain.JobAdvertAggregate.Events
{
    public class TagAddedToJobAdvert : DomainEvent
    {
        public TagAddedToJobAdvert(JobAdvert jobAdvert, Tag tag)
        {
            Check.NotNull(jobAdvert, nameof(jobAdvert));
            Check.NotNull(tag, nameof(tag));

            JobAdvert = jobAdvert;
            Tag = tag;
        }

        public JobAdvert JobAdvert { get; }
        public Tag Tag { get; }    
    }
}