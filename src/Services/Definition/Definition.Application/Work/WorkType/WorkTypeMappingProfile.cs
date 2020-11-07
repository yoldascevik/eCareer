using AutoMapper;

namespace Definition.Application.Work.WorkType
{
    public class WorkTypeMappingProfile: Profile
    {
        public WorkTypeMappingProfile()
        {
            CreateMap<Data.Entities.WorkType, WorkTypeDto>();
            CreateMap<WorkTypeRequestModel, Data.Entities.WorkType>();
        }
    }
}