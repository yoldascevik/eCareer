using AutoMapper;

namespace Definition.Application.Work.JobPosition
{
    public class JobPositionMappingProfile: Profile
    {
        public JobPositionMappingProfile()
        {
            CreateMap<Data.Entities.JobPosition, JobPositionDto>();
            CreateMap<JobPositionRequestModel, Data.Entities.JobPosition>();
        }
    }
}