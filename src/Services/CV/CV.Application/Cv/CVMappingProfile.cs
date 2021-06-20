using System.Linq;
using AutoMapper;
using CurriculumVitae.Application.Cv.Dtos;
using CurriculumVitae.Core.Entities;
using CurriculumVitae.Core.Refs;
using CurriculumVitae.Data.Entities;

namespace CurriculumVitae.Application.Cv
{
    public class CVMappingProfile : Profile
    {
        public CVMappingProfile()
        {
            CreateMap<CV, CVDto>();

            // CVSummaryDto
            CreateMap<CV, CVSummaryDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(s => s.PersonalInfo.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(s => s.PersonalInfo.LastName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(s => s.PersonalInfo.Gender))
                .ForMember(dest => dest.DisabledPerson, opt => opt.MapFrom(s => s.PersonalInfo.Disabilities.Any(x => !x.IsDeleted)));

            // SocialProfileDto
            CreateMap<SocialProfile, SocialProfileDto>();

            CreateMap<DrivingLicence, DrivingLicenceDto>();
            CreateMap<Education, EducationDto>();
            CreateMap<WorkExperience, WorkExperienceDto>();
            CreateMap<Certificate, CertificateDto>();
            CreateMap<Reference, ReferenceDto>();
            CreateMap<Attachment, AttachmentDto>();

            CreateMap<IdNameRef, IdNameRefDto>().ReverseMap();
            CreateMap<PersonLocationDto, PersonLocation>().ReverseMap();
        }
    }
}