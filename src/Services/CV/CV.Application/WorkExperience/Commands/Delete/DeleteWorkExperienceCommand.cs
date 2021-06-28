using Career.MediatR.Command;

namespace CurriculumVitae.Application.WorkExperience.Commands.Delete
{
    public class DeleteWorkExperienceCommand : ICommand
    {
        public DeleteWorkExperienceCommand(string cvId, string workExperienceId)
        {
            CvId = cvId;
            WorkExperienceId = workExperienceId;
        }

        public string CvId { get; }
        public string WorkExperienceId { get; }
    }
}