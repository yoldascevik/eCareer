using System;
using CurriculumVitae.Core.Refs;

namespace CurriculumVitae.Application.Cv.Dtos
{
    public class EducationDto
    {
        public string Id { get; set; }
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
    }
}