using AutoMapper;
using CurriculumVitae.Application.Education.Dtos;

namespace CurriculumVitae.Application.Education;

public class EducationMappingProfile : Profile
{
    public EducationMappingProfile()
    {
        CreateMap<Core.Entities.Education, EducationDto>();
        CreateMap<EducationInputDto, Core.Entities.Education>();
    }
}