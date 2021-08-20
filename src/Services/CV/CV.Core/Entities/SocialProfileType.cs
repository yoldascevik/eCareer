using Career.Domain;

namespace CurriculumVitae.Core.Entities
{
    public class SocialProfileType : EntityBase, ISoftDeletable
    {
        public string Name { get; set; }
        public string ProfileUrlPrefix { get; set; }
        public bool IsDeleted { get; set; }
    }
}