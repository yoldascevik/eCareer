using Career.Exceptions.Exceptions;

namespace CurriculumVitae.Application.Reference;

public class ReferenceNotFoundException: NotFoundException
{
    public ReferenceNotFoundException(string id)
        :base($"Reference \"{id}\" is not found!")
    { }
}