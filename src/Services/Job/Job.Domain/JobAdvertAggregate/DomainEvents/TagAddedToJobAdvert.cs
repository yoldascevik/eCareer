using Career.Domain.DomainEvent;
using Career.Exceptions;

namespace Job.Domain.JobAdvertAggregate.DomainEvents
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