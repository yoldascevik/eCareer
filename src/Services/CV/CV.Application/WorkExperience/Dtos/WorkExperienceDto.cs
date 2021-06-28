using System;
using CurriculumVitae.Application.Cv.Dtos;
using CurriculumVitae.Core.Refs;

namespace CurriculumVitae.Application.WorkExperience.Dtos
{
    public class WorkExperienceDto
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string WorkDefinition { get; set; }
        public IdNameRefDto Sector { get; set; }
        public IdNameRefDto Position { get; set; }
        public IdNameRefDto WorkType { get; set; }
    }
}