using AutoMapper;
using Job.Application.Job.Dtos;
using Job.Application.Tag.Dtos;
using Job.Domain;
using Job.Domain.JobAggregate;
using Job.Domain.JobAggregate.Refs;

namespace Job.Application.Job;

public class JobMappingProfile : Profile
{
    public JobMappingProfile()
    {
        CreateMap<Domain.JobAggregate.Job, JobDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(j => j.Status.Name))
            .IncludeAllDerived();

        CreateMap<Domain.JobAggregate.Job, JobDetailDto>();

        CreateMap<IdNameRef, IdNameRefDto>();
        CreateMap<CompanyRef, CompanyRefDto>();
        CreateMap<CandidateRef, CandidateSummaryDto>();

        CreateMap<TagRef, TagDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.TagId));

        CreateMap<JobLocation, JobLocationDto>();
    }
}