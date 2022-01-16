using CurriculumVitae.Core.Constants;
using CurriculumVitae.Core.Refs;

namespace CurriculumVitae.Core.Entities;

public class LanguageSkill : EntityBase
{
    public IdNameRef Language { get; set; }
    public LanguageSkillLevel SkillLevel { get; set; }
    public bool IsPrimary { get; set; }
}