using AutoMapper;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

namespace Definition.Application.Education.EducationLevel;

public class EducationLevelMappingProfile: Profile
{
    public EducationLevelMappingProfile()
    {
        CreateMap<Data.Entities.EducationLevel, EducationLevelDto>();
        CreateMap<EducationLevelRequestModel, Data.Entities.EducationLevel>();
    }
}