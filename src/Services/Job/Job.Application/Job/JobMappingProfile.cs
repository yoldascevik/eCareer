using AutoMapper;
using Job.Application.Job.Dtos;
using Job.Domain.JobAggregate;

namespace Job.Application.Job
{
    public class JobMappingProfile: Profile
    {
        public JobMappingProfile()
        {
            CreateMap<Domain.JobAggregate.Job, JobDto>();
            CreateMap<Domain.JobAggregate.Job, JobDetailDto>();
            
            CreateMap<TagRef, TagDto>();
            CreateMap<WorkTypeRef, WorkTypeDto>();
            CreateMap<LocationRef, JobLocationDto>();
            CreateMap<EducationLevelRef, EducationLevelDto>();
        }
    }
}