using FluentValidation;

namespace CurriculumVitae.Application.PersonalInfo.Commands.DeleteDisability
{
    public class DeleteDisabilityCommandValidator : AbstractValidator<DeleteDisabilityCommand>
    {
        public DeleteDisabilityCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}