using Career.Mongo.Document;
using CurriculumVitae.Data.Constants;
using CurriculumVitae.Data.Refs;

namespace CurriculumVitae.Data.Entities
{
    public class ForeignLanguage: Document
    {
        public IdNameRef Language { get; set; }
        public LanguageLevel Level { get; set; }
        public bool IsPrimary { get; set; }
    }
}