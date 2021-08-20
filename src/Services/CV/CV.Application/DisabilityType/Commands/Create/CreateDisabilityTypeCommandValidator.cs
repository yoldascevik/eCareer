using FluentValidation;

namespace CurriculumVitae.Application.DisabilityType.Commands.Create
{
    public class CreateDisabilityTypeCommandValidator : AbstractValidator<CreateDisabilityTypeCommand>
    {
        public CreateDisabilityTypeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}