using Career.MediatR.Command;
using CurriculumVitae.Application.WorkExperience.Dtos;

namespace CurriculumVitae.Application.WorkExperience.Commands.Update
{
    public class UpdateWorkExperienceCommand : ICommand
    {
        public UpdateWorkExperienceCommand(string cvId, string workExperienceId, WorkExperienceInputDto workExperience)
        {
            CvId = cvId;
            WorkExperienceId = workExperienceId;
            WorkExperience = workExperience;
        }

        public string CvId { get; }
        public string WorkExperienceId { get; }
        public WorkExperienceInputDto WorkExperience { get; }
    }
}