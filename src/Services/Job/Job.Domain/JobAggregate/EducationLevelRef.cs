using Career.Domain;
using Career.Exceptions;

namespace Job.Domain.JobAggregate
{
    public class EducationLevelRef : IdNameRef
    {
        private EducationLevelRef(string refId, string name) : base(refId, name)
        {
        }

        public static EducationLevelRef Create(string id, string name) => new EducationLevelRef(id, name);
    }
}