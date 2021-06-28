using System.Collections.Generic;
using Career.MediatR.Query;
using CurriculumVitae.Application.Certificate.Dtos;

namespace CurriculumVitae.Application.Certificate.Queries.Get
{
    public class GetCertificatesQuery : IQuery<List<CertificateDto>>
    {
        public GetCertificatesQuery(string cvId)
        {
            CvId = cvId;
        }

        public string CvId { get; }
    }
}