using System;
using Career.Domain;
using Career.Mongo.Document;
using CurriculumVitae.Data.Refs;

namespace CurriculumVitae.Data.Entities
{
    public class WorkExperience: Document, ISoftDeletable
    {
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string WorkDefinition { get; set; }
        public IdNameRef Sector { get; set; }
        public IdNameRef Position { get; set; }
        public IdNameRef WorkType { get; set; }
        public bool IsDeleted { get; set; }
    }
}