using AutoMapper;

namespace Definition.Application.Location.City
{
    public class CityMappingProfile: Profile
    {
        public CityMappingProfile()
        {
            CreateMap<Data.Entities.City, CityDto>();
            CreateMap<CityRequestModel, Data.Entities.City>();
        }
    }
}