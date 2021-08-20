using FluentValidation;

namespace CurriculumVitae.Application.DisabilityType.Commands.Delete
{
    public class DeleteDisabilityTypeCommandValidator : AbstractValidator<DeleteDisabilityTypeCommand>
    {
        public DeleteDisabilityTypeCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}