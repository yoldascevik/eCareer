using System;
using CurriculumVitae.Core.Refs;

namespace CurriculumVitae.Application.Cv.Dtos
{
    public class WorkExperienceDto
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string WorkDefinition { get; set; }
        public IdNameRef Sector { get; set; }
        public IdNameRef Position { get; set; }
        public IdNameRef WorkType { get; set; }
    }
}