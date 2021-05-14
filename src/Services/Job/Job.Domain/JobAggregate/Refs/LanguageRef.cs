namespace Job.Domain.JobAggregate.Refs
{
    public class LanguageRef: IdNameRef
    {
        private LanguageRef(string refId, string name) : base(refId, name)
        {
        }
        
        public static LanguageRef Create(string refId, string name) => new (refId, name);
    }
}