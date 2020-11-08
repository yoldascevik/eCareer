using AutoMapper;

namespace Definition.Application.Language
{
    public class LanguageMappingProfile: Profile
    {
        public LanguageMappingProfile()
        {
            CreateMap<Data.Entities.Language, LanguageDto>();
            CreateMap<LanguageRequestModel, Data.Entities.Language>();
        }
    }
}