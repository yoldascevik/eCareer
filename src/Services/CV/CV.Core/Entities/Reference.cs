using Career.Domain;
using Career.Mongo.Document;
using CurriculumVitae.Core.Entities;

namespace CurriculumVitae.Data.Entities
{
    public class Reference: EntityBase, ISoftDeletable
    {
        public string FullName { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsDeleted { get; set; }
    }
}