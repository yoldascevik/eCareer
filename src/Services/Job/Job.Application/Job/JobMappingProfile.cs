using AutoMapper;
using Job.Application.Job.Dtos;
using Job.Application.Tag.Dtos;
using Job.Domain.JobAggregate;

namespace Job.Application.Job
{
    public class JobMappingProfile : Profile
    {
        public JobMappingProfile()
        {
            CreateMap<Domain.JobAggregate.Job, JobDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(j => j.Status.Name))
                .IncludeAllDerived();

            CreateMap<Domain.JobAggregate.Job, JobDetailDto>();
            CreateMap<CandidateRef, CandidateSummaryDto>();

            CreateMap<TagRef, TagDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.TagId));

            CreateMap<WorkTypeRef, WorkTypeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.WorkTypeId));

            CreateMap<EducationLevelRef, EducationLevelDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.RefId));

            CreateMap<LocationRef, JobLocationDto>();
        }
    }
}