using Career.Domain;

namespace CurriculumVitae.Core.Entities
{
    public class SocialProfile : EntityBase, ISoftDeletable
    {
        public string TypeId { get; set; }
        public string Username { get; set; }
        public bool IsDeleted { get; set; }
    }
}