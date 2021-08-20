using FluentValidation;

namespace CurriculumVitae.Application.Reference.Commands.Add
{
    public class AddReferenceCommandValidator : AbstractValidator<AddReferenceCommand>
    {
        public AddReferenceCommandValidator()
        {
            RuleFor(x => x.CvId).NotEmpty();
            RuleFor(x => x.Reference).SetValidator(new ReferenceInputDtoValidator());
        }
    }
 }