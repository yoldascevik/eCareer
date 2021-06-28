using Career.Exceptions.Exceptions;

namespace CurriculumVitae.Application.Education
{
    public class EducationNotFoundException: NotFoundException
    {
        public EducationNotFoundException(string id)
            :base($"Education \"{id}\" is not found!")
        { }
    }
}