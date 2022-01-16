using Career.Exceptions.Exceptions;

namespace CurriculumVitae.Application.DisabilityType;

public class DisabilityTypeNotFoundException: NotFoundException
{
    public DisabilityTypeNotFoundException(string id)
        :base($"Disability type \"{id}\" is not found!")
    { }
}