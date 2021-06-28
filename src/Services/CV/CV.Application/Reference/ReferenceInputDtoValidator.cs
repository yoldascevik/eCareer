using CurriculumVitae.Application.Cv.Validators;
using CurriculumVitae.Application.Reference.Dtos;
using FluentValidation;

namespace CurriculumVitae.Application.Reference
{
    public class ReferenceInputDtoValidator : AbstractValidator<ReferenceInputDto>
    {
        public ReferenceInputDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Phone).NotEmpty();
        }
    }
}