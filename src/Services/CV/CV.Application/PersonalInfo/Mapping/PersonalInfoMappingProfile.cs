using System.Linq;
using AutoMapper;
using CurriculumVitae.Application.PersonalInfo.Dtos;

namespace CurriculumVitae.Application.PersonalInfo.Mapping
{
    public class PersonalInfoMappingProfile : Profile
    {
        public PersonalInfoMappingProfile()
        {
            CreateMap<Core.Entities.PersonalInfo, PersonalInfoDto>()
                .ForMember(dest => dest.DisabledPerson, opt => opt.MapFrom(s => s.Disabilities.Any()))
                .ReverseMap();
        }
    }
}