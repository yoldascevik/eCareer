using Career.MediatR.Query;
using CurriculumVitae.Application.WorkExperience.Dtos;

namespace CurriculumVitae.Application.WorkExperience.Queries.GetById
{
    public class GetWorkExperienceByIdQuery : IQuery<WorkExperienceDto>
    {
        public GetWorkExperienceByIdQuery(string cvId, string workExperienceId)
        {
            CvId = cvId;
            WorkExperienceId = workExperienceId;
        }

        public string CvId { get; }
        public string WorkExperienceId { get; }
    }
}