using AutoMapper;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

namespace Definition.Application.Work.Sector
{
    public class SectorMappingProfile: Profile
    {
        public SectorMappingProfile()
        {
            CreateMap<Data.Entities.Sector, SectorDto>();
            CreateMap<SectorRequestModel, Data.Entities.Sector>();
        }
    }
}