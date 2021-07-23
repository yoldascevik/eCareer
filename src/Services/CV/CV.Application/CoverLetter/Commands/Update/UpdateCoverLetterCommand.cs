using Career.MediatR.Command;
using CurriculumVitae.Application.CoverLetter.Dtos;

namespace CurriculumVitae.Application.CoverLetter.Commands.Update
{
    public class UpdateCoverLetterCommand: ICommand
    {
        public UpdateCoverLetterCommand(string coverLetterId, CoverLetterInputDto coverLetter)
        {
            CoverLetterId = coverLetterId;
            CoverLetter = coverLetter;
        }

        public string CoverLetterId { get; }
        public CoverLetterInputDto CoverLetter { get; }
    }
}