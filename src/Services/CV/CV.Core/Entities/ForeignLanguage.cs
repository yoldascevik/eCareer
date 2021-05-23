using CurriculumVitae.Core.Constants;
using CurriculumVitae.Core.Refs;

namespace CurriculumVitae.Core.Entities
{
    public class ForeignLanguage : EntityBase
    {
        public IdNameRef Language { get; set; }
        public LanguageLevel Level { get; set; }
        public bool IsPrimary { get; set; }
    }
}