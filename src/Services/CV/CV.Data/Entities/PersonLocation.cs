using CurriculumVitae.Data.Refs;

namespace CurriculumVitae.Data.Entities
{
    public class PersonLocation
    {
        public IdNameRef Country { get; set; }
        public IdNameRef City { get; set; }
    }
}