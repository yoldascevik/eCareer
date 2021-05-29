using System.Linq;
using AutoMapper;
using Career.Domain.Extensions;
using CurriculumVitae.Application.PersonalInfo.Dtos;

namespace CurriculumVitae.Application.PersonalInfo.Mapping
{
    public class PersonalInfoMappingProfile : Profile
    {
        public PersonalInfoMappingProfile()
        {
            CreateMap<Core.Entities.PersonalInfo, PersonalInfoDto>()
                .ForMember(dest => dest.DisabledPerson, opt => opt.MapFrom(s => s.Disabilities.ExcludeDeletedItems().Any()))
                .ReverseMap();
        }
    }
}