using System;
using Career.Domain;
using Career.Mongo.Document;

namespace CurriculumVitae.Data.Entities
{
    public class CoverLetter: Document, ISoftDeletable
    {
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public DateTime LastModificationTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}