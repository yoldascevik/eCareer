using AutoMapper;
using CurriculumVitae.Application.DisabilityType.Dtos;
using CurriculumVitae.Application.PersonalInfo.Dtos;

namespace CurriculumVitae.Application.PersonalInfo.Mapping
{
    public class DisabilityMappingProfile: Profile
    {
        public DisabilityMappingProfile()
        {
            CreateMap<Core.Entities.Disability, DisabilityDto>();
            CreateMap<Core.Entities.DisabilityType, DisabilityTypeDto>();
            CreateMap<DisabilityInputDto, Core.Entities.Disability>();
        }
    }
}