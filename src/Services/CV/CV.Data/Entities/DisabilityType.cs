using Career.Domain;
using Career.Mongo.Document;

namespace CurriculumVitae.Data.Entities
{
    public class DisabilityType: Document, ISoftDeletable
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}