using Career.Exceptions.Exceptions;

namespace CurriculumVitae.Application.CoverLetter
{
    public class CoverLetterNotFoundException : NotFoundException
    {
        public CoverLetterNotFoundException(string id)
            :base($"Cover letter \"{id}\" is not found!")
        { }
    }
}