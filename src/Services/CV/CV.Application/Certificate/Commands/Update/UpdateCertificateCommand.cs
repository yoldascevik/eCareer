using Career.MediatR.Command;
using CurriculumVitae.Application.Certificate.Dtos;

namespace CurriculumVitae.Application.Certificate.Commands.Update
{
    public class UpdateCertificateCommand : ICommand
    {
        public UpdateCertificateCommand(string cvId, string certificateId, CertificateInputDto certificate)
        {
            CvId = cvId;
            CertificateId = certificateId;
            Certificate = certificate;
        }

        public string CvId { get; }
        public string CertificateId { get; }
        public CertificateInputDto Certificate { get; }
    }
}