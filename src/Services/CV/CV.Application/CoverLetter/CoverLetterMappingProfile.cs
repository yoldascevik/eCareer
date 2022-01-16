using AutoMapper;
using CurriculumVitae.Application.CoverLetter.Dtos;

namespace CurriculumVitae.Application.CoverLetter;

public class CoverLetterMappingProfile : Profile
{
    public CoverLetterMappingProfile()
    {
        CreateMap<Core.Entities.CoverLetter, CoverLetterDto>();
        CreateMap<CoverLetterInputDto, Core.Entities.CoverLetter>();
    }
}