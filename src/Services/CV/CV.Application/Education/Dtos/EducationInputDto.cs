using System;
using CurriculumVitae.Application.Cv.Dtos;

namespace CurriculumVitae.Application.Education.Dtos;

public class EducationInputDto
{
    public string SchoolName { get; set; }
    public string Section { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public float Degree { get; set; }
    public bool IsAbondonment { get; set; }
    public IdNameRefDto EducationLevel { get; set; }
    public IdNameRefDto EducationType { get; set; }
    public IdNameRefDto Language { get; set; }
    public IdNameRefDto ScholarshipType { get; set; }
}