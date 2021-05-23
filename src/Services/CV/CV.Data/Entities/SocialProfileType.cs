using Career.Domain;
using Career.Mongo.Document;

namespace CurriculumVitae.Data.Entities
{
    public class SocialProfileType: Document, ISoftDeletable
    {
        public string Name { get; set; }
        public string ProfileUrlPrefix { get; set; }
        public bool IsDeleted { get; set; }
    }
}