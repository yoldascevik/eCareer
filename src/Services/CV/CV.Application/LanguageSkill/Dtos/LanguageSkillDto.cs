using CurriculumVitae.Application.Cv.Dtos;
using CurriculumVitae.Core.Constants;

namespace CurriculumVitae.Application.LanguageSkill.Dtos
{
    public class LanguageSkillDto
    {
        public string Id { get; set; }
        public IdNameRefDto Language { get; set; }
        public LanguageSkillLevel SkillLevel { get; set; }
        public bool IsPrimary { get; set; }
    }
}