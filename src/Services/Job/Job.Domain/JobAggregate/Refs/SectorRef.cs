namespace Job.Domain.JobAggregate.Refs
{
    public class SectorRef: IdNameRef
    {
        private SectorRef(string refId, string name) : base(refId, name)
        {
        }
        
        public static SectorRef Create(string refId, string name) => new (refId, name);
    }
}