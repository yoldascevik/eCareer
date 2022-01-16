using FluentValidation;

namespace CurriculumVitae.Application.DisabilityType.Commands.Update;

public class UpdateDisabilityTypeCommandValidator : AbstractValidator<UpdateDisabilityTypeCommand>
{
    public UpdateDisabilityTypeCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}