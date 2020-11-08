using AutoMapper;

namespace Definition.Application.Education.ScholarshipType
{
    public class ScholarshipTypeMappingProfile: Profile
    {
        public ScholarshipTypeMappingProfile()
        {
            CreateMap<Data.Entities.ScholarshipType, ScholarshipTypeDto>();
            CreateMap<ScholarshipTypeRequestModel, Data.Entities.ScholarshipType>();
        }
    }
}