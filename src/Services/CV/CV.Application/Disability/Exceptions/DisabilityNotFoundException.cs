using Career.Exceptions;
using Career.Exceptions.Exceptions;

namespace CurriculumVitae.Application.Disability.Exceptions
{
    public class DisabilityNotFoundException: NotFoundException
    {
        public DisabilityNotFoundException(string id)
            :base($"Disability \"{id}\" is not found!")
        { }
    }
}