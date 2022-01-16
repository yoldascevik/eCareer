using Career.Domain;

namespace CurriculumVitae.Core.Constants;

public class LanguageSkillLevel: Enumeration<string>
{
    private LanguageSkillLevel(string id, string name) : base(id, name)
    {
    }
        
    public static readonly LanguageSkillLevel A1 = new ("A1", "Beginner");
    public static readonly LanguageSkillLevel A2 = new ("A2", "Elementary");
    public static readonly LanguageSkillLevel B1 = new ("B1", "Intermediate");
    public static readonly LanguageSkillLevel B2 = new ("B2", "Upper Intermediate");
    public static readonly LanguageSkillLevel C1 = new ("C1", "Advanced");
    public static readonly LanguageSkillLevel C2 = new ("C2", "Proficient");
}