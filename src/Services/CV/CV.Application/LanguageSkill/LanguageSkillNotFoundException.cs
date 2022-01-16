using Career.Exceptions.Exceptions;

namespace CurriculumVitae.Application.LanguageSkill;

public class LanguageSkillNotFoundException: NotFoundException
{
    public LanguageSkillNotFoundException(string id)
        :base($"Language skill \"{id}\" is not found!")
    { }
}