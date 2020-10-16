using AutoMapper;
using Definition.Application.Dtos;
using Definition.Application.Models.RequestModels;

namespace Definition.Application.MappingProfiles
{
    public class CountryMappingProfile: Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<Data.Entities.Country, CountryDto>();
            CreateMap<CountryRequestModel, Data.Entities.Country>();
        }
    }
}