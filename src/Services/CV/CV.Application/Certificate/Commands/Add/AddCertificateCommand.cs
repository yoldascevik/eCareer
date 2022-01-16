using Career.MediatR.Command;
using CurriculumVitae.Application.Certificate.Dtos;

namespace CurriculumVitae.Application.Certificate.Commands.Add;

public class AddCertificateCommand : ICommand<CertificateDto>
{
    public AddCertificateCommand(string cvId, CertificateInputDto certificate)
    {
        CvId = cvId;
        Certificate = certificate;
    }

    public string CvId { get; }
    public CertificateInputDto Certificate { get; }
}