using AutoMapper;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

namespace Definition.Application.Location.Country
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