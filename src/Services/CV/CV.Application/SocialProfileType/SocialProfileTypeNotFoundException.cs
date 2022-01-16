using Career.Exceptions.Exceptions;

namespace CurriculumVitae.Application.SocialProfileType;

public class SocialProfileTypeNotFoundException: NotFoundException
{
    public SocialProfileTypeNotFoundException(string id)
        :base($"Social profile type \"{id}\" is not found!")
    { }
}