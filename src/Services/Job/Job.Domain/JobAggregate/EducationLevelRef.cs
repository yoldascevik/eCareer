namespace Job.Domain.JobAggregate
{
    public class EducationLevelRef : IdNameRef
    {
        private EducationLevelRef(string refId, string name) : base(refId, name)
        {
        }

        public new static EducationLevelRef Create(string id, string name) => new(id, name);
    }
}