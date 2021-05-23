using System;
using Career.Domain;
using Career.Mongo.Document;

namespace CurriculumVitae.Data.Entities
{
    public class Attachment: Document, ISoftDeletable
    {
        public string Description { get; set; }
        public string Path { get; set; }
        public DateTime UploadDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}