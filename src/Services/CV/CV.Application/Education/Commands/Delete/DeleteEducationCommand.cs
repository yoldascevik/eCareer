using Career.MediatR.Command;

namespace CurriculumVitae.Application.Education.Commands.Delete;

public class DeleteEducationCommand : ICommand
{
    public DeleteEducationCommand(string cvId, string educationId)
    {
        CvId = cvId;
        EducationId = educationId;
    }

    public string CvId { get; }
    public string EducationId { get; }
}