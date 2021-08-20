using FluentValidation;

namespace CurriculumVitae.Application.Reference.Commands.Update
{
    public class UpdateReferenceCommandValidator : AbstractValidator<UpdateReferenceCommand>
    {
        public UpdateReferenceCommandValidator()
        {
            RuleFor(x => x.CvId).NotEmpty();
            RuleFor(x => x.ReferenceId).NotEmpty();
            RuleFor(x => x.Reference).SetValidator(new ReferenceInputDtoValidator());
        }
    }
}