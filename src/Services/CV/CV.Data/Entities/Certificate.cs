using System;
using Career.Domain;
using Career.Mongo.Document;

namespace CurriculumVitae.Data.Entities
{
    public class Certificate: Document, ISoftDeletable
    {
        public string Name { get; set; }
        public string Institution { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
    }
}