using FluentValidation;

namespace CurriculumVitae.Application.Disability.Commands.Delete
{
    public class DeleteDisabilityCommandValidator : AbstractValidator<DeleteDisabilityCommand>
    {
        public DeleteDisabilityCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}