using AutoMapper;
using CurriculumVitae.Application.DrivingLicence.Dtos;

namespace CurriculumVitae.Application.DrivingLicence
{
    public class DrivingLicenceMappingProfile : Profile
    {
        public DrivingLicenceMappingProfile()
        {
            CreateMap<Core.Entities.DrivingLicence, DrivingLicenceDto>();
            CreateMap<DrivingLicenceInputDto, Core.Entities.DrivingLicence>();
        }
    }
}