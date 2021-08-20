using FluentValidation;

namespace CurriculumVitae.Application.CoverLetter.Commands.Update
{
    public class UpdateCoverLetterCommandValidator : AbstractValidator<UpdateCoverLetterCommand>
    {
        public UpdateCoverLetterCommandValidator()
        {
            RuleFor(x => x.CoverLetterId).NotEmpty();
            RuleFor(x => x.CoverLetter).SetValidator(new CoverLetterInputDtoValidator());
        }
    }
}