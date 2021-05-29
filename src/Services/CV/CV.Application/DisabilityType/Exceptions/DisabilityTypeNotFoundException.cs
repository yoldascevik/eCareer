using Career.Exceptions;

namespace CurriculumVitae.Application.DisabilityType.Exceptions
{
    public class DisabilityTypeNotFoundException: CareerExceptionBase
    {
        public DisabilityTypeNotFoundException(string id)
            :base($"Disability type \"{id}\" is not found!")
        { }
    }
}