using Career.Domain;
using Career.Mongo.Document;

namespace CurriculumVitae.Data.Entities
{
    public class SocialProfile: Document, ISoftDeletable
    {
        public string TypeId { get; set; }
        public string Username { get; set; }
        public bool IsDeleted { get; set; }
    }
}