using AutoMapper;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

namespace Definition.Application.Education.EducationType;

public class EducationTypeMappingProfile: Profile
{
    public EducationTypeMappingProfile()
    {
        CreateMap<Data.Entities.EducationType, EducationTypeDto>();
        CreateMap<EducationTypeRequestModel, Data.Entities.EducationType>();
    }
}