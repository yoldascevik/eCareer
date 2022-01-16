using AutoMapper;
using Definition.Contract.Dto;
using Definition.Contract.RequestModel;

namespace Definition.Application.Work.JobPosition;

public class JobPositionMappingProfile: Profile
{
    public JobPositionMappingProfile()
    {
        CreateMap<Data.Entities.JobPosition, JobPositionDto>();
        CreateMap<JobPositionRequestModel, Data.Entities.JobPosition>();
    }
}