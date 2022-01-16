using Career.Exceptions.Exceptions;

namespace CurriculumVitae.Application.SocialProfile;

public class SocialProfileNotFoundException: NotFoundException
{
    public SocialProfileNotFoundException(string id)
        :base($"Social profile \"{id}\" is not found!")
    { }
}