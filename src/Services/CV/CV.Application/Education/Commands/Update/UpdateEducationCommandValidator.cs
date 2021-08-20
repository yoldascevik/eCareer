using FluentValidation;

namespace CurriculumVitae.Application.Education.Commands.Update
{
    public class UpdateEducationCommandValidator : AbstractValidator<UpdateEducationCommand>
    {
        public UpdateEducationCommandValidator()
        {
            RuleFor(x => x.CvId).NotEmpty();
            RuleFor(x => x.EducationId).NotEmpty();
            RuleFor(x => x.Education).SetValidator(new EducationInputDtoValidator());
        }
    }
}