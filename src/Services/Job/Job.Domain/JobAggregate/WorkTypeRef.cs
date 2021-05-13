using Career.Domain.Entities;
using Career.Exceptions;

namespace Job.Domain.JobAggregate
{
    public class WorkTypeRef : IdNameRef
    {
        private WorkTypeRef(string refId, string name) : base(refId, name)
        {
        }

        public static WorkTypeRef Create(string id, string name) => new WorkTypeRef(id, name);
    }
}