using AutoMapper;
using CurriculumVitae.Application.PersonalInfo.Dtos;

namespace CurriculumVitae.Application.PersonalInfo.Mapping
{
    public class PersonalInfoMappingProfile: Profile
    {
        public PersonalInfoMappingProfile()
        {
            CreateMap<Core.Entities.PersonalInfo, PersonalInfoDto>().ReverseMap();
        }
    }
}