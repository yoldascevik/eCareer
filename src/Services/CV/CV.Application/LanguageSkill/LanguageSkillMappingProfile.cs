using AutoMapper;
using CurriculumVitae.Application.LanguageSkill.Dtos;
using CurriculumVitae.Core.Constants;

namespace CurriculumVitae.Application.LanguageSkill;

public class LanguageSkillMappingProfile : Profile
{
    public LanguageSkillMappingProfile()
    {
        CreateMap<Core.Entities.LanguageSkill, LanguageSkillDto>();
        CreateMap<LanguageSkillInputDto, Core.Entities.LanguageSkill>()
            .ForMember(dest => dest.SkillLevel, opt => opt.MapFrom(s => LanguageSkillLevel.FromValue<LanguageSkillLevel>(s.SkillLevel)));
            
        CreateMap<UpdateLanguageSkillDto, Core.Entities.LanguageSkill>()
            .ForMember(dest => dest.SkillLevel, opt => opt.MapFrom(s => LanguageSkillLevel.FromValue<LanguageSkillLevel>(s.SkillLevel)));
    }
}