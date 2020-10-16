using AutoMapper;
using Definition.Application.Dtos;
using Definition.Application.Models.RequestModels;

namespace Definition.Application.MappingProfiles
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