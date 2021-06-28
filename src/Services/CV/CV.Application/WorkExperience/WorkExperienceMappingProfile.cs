using AutoMapper;
using CurriculumVitae.Application.WorkExperience.Dtos;

namespace CurriculumVitae.Application.WorkExperience
{
    public class WorkExperienceMappingProfile : Profile
    {
        public WorkExperienceMappingProfile()
        {
            CreateMap<Core.Entities.WorkExperience, WorkExperienceDto>();
            CreateMap<WorkExperienceInputDto, Core.Entities.WorkExperience>();
        }
    }
}