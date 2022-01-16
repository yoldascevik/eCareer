using AutoMapper;
using CurriculumVitae.Application.Reference.Dtos;

namespace CurriculumVitae.Application.Reference;

public class ReferenceMappingProfile : Profile
{
    public ReferenceMappingProfile()
    {
        CreateMap<Data.Entities.Reference, ReferenceDto>();
        CreateMap<ReferenceInputDto, Data.Entities.Reference>();
    }
}