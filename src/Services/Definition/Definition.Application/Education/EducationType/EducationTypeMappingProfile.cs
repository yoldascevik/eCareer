using AutoMapper;

namespace Definition.Application.Education.EducationType
{
    public class EducationTypeMappingProfile: Profile
    {
        public EducationTypeMappingProfile()
        {
            CreateMap<Data.Entities.EducationType, EducationTypeDto>();
            CreateMap<EducationTypeRequestModel, Data.Entities.EducationType>();
        }
    }
}