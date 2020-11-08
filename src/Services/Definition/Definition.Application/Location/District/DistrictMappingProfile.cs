using AutoMapper;

namespace Definition.Application.Location.District
{
    public class DistrictMappingProfile: Profile
    {
        public DistrictMappingProfile()
        {
            CreateMap<Data.Entities.District, DistrictDto>();
            CreateMap<DistrictRequestModel, Data.Entities.District>();
        }
    }
}