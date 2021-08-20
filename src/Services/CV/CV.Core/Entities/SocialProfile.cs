using Career.Domain;
using CurriculumVitae.Core.Refs;

namespace CurriculumVitae.Core.Entities
{
    public class SocialProfile : EntityBase, ISoftDeletable
    {
        public SocialProfileTypeRef Type { get; set; }
        public string Username { get; set; }
        public bool IsDeleted { get; set; }
    }
}