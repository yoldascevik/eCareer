using AutoMapper;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

namespace Definition.Application.Location.City;

public class CityMappingProfile: Profile
{
    public CityMappingProfile()
    {
        CreateMap<Data.Entities.City, CityDto>();
        CreateMap<CityRequestModel, Data.Entities.City>();
    }
}