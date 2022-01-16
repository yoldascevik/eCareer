using Career.MediatR.Query;
using CurriculumVitae.Application.Certificate.Dtos;

namespace CurriculumVitae.Application.Certificate.Queries.GetById;

public class GetCertificateByIdQuery : IQuery<CertificateDto>
{
    public GetCertificateByIdQuery(string cvId, string certificateId)
    {
        CvId = cvId;
        CertificateId = certificateId;
    }

    public string CvId { get; }
    public string CertificateId { get; }
}