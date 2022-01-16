using Career.MediatR.Command;

namespace CurriculumVitae.Application.Certificate.Commands.Delete;

public class DeleteCertificateCommand : ICommand
{
    public DeleteCertificateCommand(string cvId, string certificateId)
    {
        CvId = cvId;
        CertificateId = certificateId;
    }

    public string CvId { get; }
    public string CertificateId { get; }
}