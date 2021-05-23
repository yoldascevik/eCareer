using System;
using Career.Domain;
using Career.Mongo.Document;
using CurriculumVitae.Data.Refs;

namespace CurriculumVitae.Data.Entities
{
    public class Education: Document, ISoftDeletable
    {
        public string SchoolName { get; set; }
        public string Section { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public float Degree { get; set; }
        public bool IsAbondonment { get; set; }
        public IdNameRef EducationLevel { get; set; }
        public IdNameRef EducationType { get; set; }
        public IdNameRef Language { get; set; }
        public IdNameRef ScholarshipType { get; set; }
        public bool IsDeleted { get; set; }
    }
}