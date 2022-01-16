using CurriculumVitae.Core.Refs;

namespace CurriculumVitae.Core.Entities;

public class PersonLocation
{
    public IdNameRef Country { get; set; }
    public IdNameRef City { get; set; }
}