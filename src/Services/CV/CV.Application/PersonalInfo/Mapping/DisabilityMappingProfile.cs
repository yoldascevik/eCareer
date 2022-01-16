using AutoMapper;
using CurriculumVitae.Application.DisabilityType;
using CurriculumVitae.Application.PersonalInfo.Dtos;
using CurriculumVitae.Core.Refs;

namespace CurriculumVitae.Application.PersonalInfo.Mapping;

public class DisabilityMappingProfile: Profile
{
    public DisabilityMappingProfile()
    {
        CreateMap<Core.Entities.Disability, DisabilityDto>();
        CreateMap<DisabilityInputDto, Core.Entities.Disability>();
            
        CreateMap<DisabilityTypeRef, DisabilityTypeDto>();
        CreateMap<Core.Entities.DisabilityType, DisabilityTypeDto>();
        CreateMap<Core.Entities.DisabilityType, DisabilityTypeRef>();
    }
}