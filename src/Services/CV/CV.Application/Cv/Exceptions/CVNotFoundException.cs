using Career.Exceptions;

namespace CurriculumVitae.Application.Cv.Exceptions
{
    public class CVNotFoundException: CareerExceptionBase
    {
        public CVNotFoundException(string id)
            :base($"CV \"{id}\" is not found!")
        { }
    }
}