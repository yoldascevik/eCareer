using AutoMapper;
using CurriculumVitae.Application.Certificate.Dtos;

namespace CurriculumVitae.Application.Certificate
{
    public class CertificateMappingProfile : Profile
    {
        public CertificateMappingProfile()
        {
            CreateMap<Core.Entities.Certificate, CertificateDto>();
            CreateMap<CertificateInputDto, Core.Entities.Certificate>();
        }
    }
}