using FluentValidation;

namespace CurriculumVitae.Application.Education.Commands.Add;

public class AddEducationCommandValidator : AbstractValidator<AddEducationCommand>
{
    public AddEducationCommandValidator()
    {
        RuleFor(x => x.CvId).NotEmpty();
        RuleFor(x => x.Education).SetValidator(new EducationInputDtoValidator());
    }
}