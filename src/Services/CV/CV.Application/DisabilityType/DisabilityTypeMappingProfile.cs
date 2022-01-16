using AutoMapper;

namespace CurriculumVitae.Application.DisabilityType;

public class DisabilityTypeMappingProfile : Profile
{
    public DisabilityTypeMappingProfile()
    {
        CreateMap<Core.Entities.DisabilityType, DisabilityTypeDto>();
    }
}