using System;
using CurriculumVitae.Application.Cv.Dtos;

namespace CurriculumVitae.Application.WorkExperience.Dtos
{
    public class WorkExperienceInputDto
    {
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string WorkDefinition { get; set; }
        public IdNameRefDto Sector { get; set; }
        public IdNameRefDto Position { get; set; }
        public IdNameRefDto WorkType { get; set; }
    }
}