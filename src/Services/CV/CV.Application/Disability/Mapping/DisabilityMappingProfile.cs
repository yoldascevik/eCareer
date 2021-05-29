using AutoMapper;
using CurriculumVitae.Application.Cv.Dtos;
using CurriculumVitae.Application.Disability.Dtos;
using CurriculumVitae.Application.DisabilityType.Dtos;
using CurriculumVitae.Core.Entities;
using CurriculumVitae.Data.Entities;

namespace CurriculumVitae.Application.Disability.Mapping
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