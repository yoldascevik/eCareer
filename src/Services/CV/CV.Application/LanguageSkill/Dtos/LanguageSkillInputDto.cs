using CurriculumVitae.Application.Cv.Dtos;

namespace CurriculumVitae.Application.LanguageSkill.Dtos;

public class LanguageSkillInputDto
{
    public IdNameRefDto Language { get; set; }
    public string SkillLevel { get; set; }
    public bool IsPrimary { get; set; }
}