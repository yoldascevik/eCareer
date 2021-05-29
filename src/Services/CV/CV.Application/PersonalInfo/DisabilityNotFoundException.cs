using Career.Exceptions.Exceptions;

namespace CurriculumVitae.Application.PersonalInfo
{
    public class DisabilityNotFoundException: NotFoundException
    {
        public DisabilityNotFoundException(string id)
            :base($"Disability \"{id}\" is not found!")
        { }
    }
}