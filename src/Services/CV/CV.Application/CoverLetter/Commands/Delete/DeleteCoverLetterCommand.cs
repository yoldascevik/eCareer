using Career.MediatR.Command;

namespace CurriculumVitae.Application.CoverLetter.Commands.Delete;

public class DeleteCoverLetterCommand : ICommand
{
    public DeleteCoverLetterCommand(string coverLetterId)
    {
        CoverLetterId = coverLetterId;
    }

    public string CoverLetterId { get; }
}