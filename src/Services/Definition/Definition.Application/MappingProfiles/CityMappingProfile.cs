using AutoMapper;
using Definition.Application.Dtos;
using Definition.Application.Models.RequestModels;

namespace Definition.Application.MappingProfiles
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