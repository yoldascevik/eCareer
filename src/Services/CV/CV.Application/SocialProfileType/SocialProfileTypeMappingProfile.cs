using AutoMapper;
using CurriculumVitae.Application.SocialProfileType.Dtos;

namespace CurriculumVitae.Application.SocialProfileType
{
    public class SocialProfileTypeMappingProfile : Profile
    {
        public SocialProfileTypeMappingProfile()
        {
            CreateMap<Core.Entities.SocialProfileType, SocialProfileTypeDto>();
        }
    }
}