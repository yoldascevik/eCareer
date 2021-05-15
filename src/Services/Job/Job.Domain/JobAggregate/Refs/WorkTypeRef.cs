namespace Job.Domain.JobAggregate.Refs
{
    public class WorkTypeRef: IdNameRef
    {
        private WorkTypeRef(string refId, string name) : base(refId, name)
        {
        }
        
        public static WorkTypeRef Create(string refId, string name) => new (refId, name);
    }
}