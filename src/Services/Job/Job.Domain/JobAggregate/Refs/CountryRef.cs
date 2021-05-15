namespace Job.Domain.JobAggregate.Refs
{
    public class CountryRef: IdNameRef
    {
        private CountryRef(string refId, string name) : base(refId, name)
        {
        }
        
        public static CountryRef Create(string refId, string name) => new (refId, name);
    }
}