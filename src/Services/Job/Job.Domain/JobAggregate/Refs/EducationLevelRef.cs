namespace Job.Domain.JobAggregate.Refs
{
    public class EducationLevelRef: IdNameRef
    {
        private EducationLevelRef(string refId, string name) : base(refId, name)
        {
        }
        
        public static EducationLevelRef Create(string refId, string name) => new (refId, name);
    }
}