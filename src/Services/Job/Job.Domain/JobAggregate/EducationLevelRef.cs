using Career.Domain;
using Career.Exceptions;

namespace Job.Domain.JobAggregate
{
    public class EducationLevelRef : ValueObject
    {
        public string EducationLevelId { get; private init; }
        public string Name { get; private set; }

        public static EducationLevelRef Create(string id, string name)
        {
            Check.NotNullOrEmpty(id, nameof(id));
            Check.NotNullOrEmpty(name, nameof(name));

            return new EducationLevelRef() {EducationLevelId = id, Name = name};
        }
        
        public EducationLevelRef SetName(string name)
        {
            Check.NotNullOrEmpty(name, nameof(name));
            Name = name;

            return this;
        }
    }
}