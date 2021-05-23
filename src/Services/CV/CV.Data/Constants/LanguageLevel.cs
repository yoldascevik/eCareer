using Career.Domain;

namespace CurriculumVitae.Data.Constants
{
    public class LanguageLevel: Enumeration<string>
    {
        private LanguageLevel(string id, string name) : base(id, name)
        {
        }
        
        public static readonly LanguageLevel A1 = new ("A1", "Beginner");
        public static readonly LanguageLevel A2 = new ("A2", "Elementary");
        public static readonly LanguageLevel B1 = new ("B1", "Intermediate");
        public static readonly LanguageLevel B2 = new ("B2", "Upper Intermediate");
        public static readonly LanguageLevel C1 = new ("C1", "Advanced");
        public static readonly LanguageLevel C2 = new ("C2", "Proficient");
    }
}