using AutoMapper;

namespace Definition.Application.Education.EducationLevel
{
    public class EducationLevelMappingProfile: Profile
    {
        public EducationLevelMappingProfile()
        {
            CreateMap<Data.Entities.EducationLevel, EducationLevelDto>();
            CreateMap<EducationLevelRequestModel, Data.Entities.EducationLevel>();
        }
    }
}