using AutoMapper;
using CurriculumVitae.Application.SocialProfile.Dtos;

namespace CurriculumVitae.Application.SocialProfile;

public class SocialProfileMappingProfile : Profile
{
    public SocialProfileMappingProfile()
    {
        CreateMap<Core.Entities.SocialProfile, SocialProfileDto>()
            .ForMember(dest => dest.ProfileUrl, opt => opt.MapFrom(s => $"{s.Type.ProfileUrlPrefix}{s.Username}"));

        CreateMap<SocialProfileInputDto, Core.Entities.SocialProfile>();
    }
}