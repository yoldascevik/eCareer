using AutoMapper;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

namespace Definition.Application.Language;

public class LanguageMappingProfile: Profile
{
    public LanguageMappingProfile()
    {
        CreateMap<Data.Entities.Language, LanguageDto>();
        CreateMap<LanguageRequestModel, Data.Entities.Language>();
    }
}