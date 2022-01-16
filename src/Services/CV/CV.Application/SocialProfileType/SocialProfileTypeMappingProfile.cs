using AutoMapper;
using CurriculumVitae.Application.SocialProfileType.Dtos;
using CurriculumVitae.Core.Refs;

namespace CurriculumVitae.Application.SocialProfileType;

public class SocialProfileTypeMappingProfile : Profile
{
    public SocialProfileTypeMappingProfile()
    {
        CreateMap<Core.Entities.SocialProfileType, SocialProfileTypeDto>();
        CreateMap<Core.Entities.SocialProfileType, SocialProfileTypeRef>();
    }
}