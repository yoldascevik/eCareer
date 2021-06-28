using Career.Exceptions.Exceptions;

namespace CurriculumVitae.Application.Certificate
{
    public class CertificateNotFoundException: NotFoundException
    {
        public CertificateNotFoundException(string id)
            :base($"Certificate \"{id}\" is not found!")
        { }
    }
}