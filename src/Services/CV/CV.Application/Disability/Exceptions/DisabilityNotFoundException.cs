using Career.Exceptions;

namespace CurriculumVitae.Application.Disability.Exceptions
{
    public class DisabilityNotFoundException: CareerExceptionBase
    {
        public DisabilityNotFoundException(string id)
            :base($"Disability \"{id}\" is not found!")
        { }
    }
}