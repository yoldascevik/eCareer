using Career.Exceptions.Exceptions;

namespace CurriculumVitae.Application.Cv;

public class CVNotFoundException: NotFoundException
{
    public CVNotFoundException(string id)
        :base($"CV \"{id}\" is not found!")
    { }
}