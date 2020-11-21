using AutoMapper;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

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