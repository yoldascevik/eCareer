namespace Job.Domain.JobAggregate.Refs
{
    public class JobPositionRef: IdNameRef
    {
        private JobPositionRef(string refId, string name) : base(refId, name)
        {
        }
        
        public static JobPositionRef Create(string refId, string name) => new (refId, name);
    }
}