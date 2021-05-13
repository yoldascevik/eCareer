namespace Job.Domain.JobAggregate
{
    public class WorkTypeRef : IdNameRef
    {
        private WorkTypeRef(string refId, string name) : base(refId, name)
        {
        }

        public new static WorkTypeRef Create(string id, string name) => new(id, name);
    }
}