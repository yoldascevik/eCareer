using Career.Exceptions.Exceptions;

namespace CurriculumVitae.Application.WorkExperience
{
    public class WorkExperienceNotFoundException: NotFoundException
    {
        public WorkExperienceNotFoundException(string id)
            :base($"WorkExperience \"{id}\" is not found!")
        { }
    }
}