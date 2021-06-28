using Career.Shared.Timing;
using CurriculumVitae.Application.Certificate.Dtos;
using CurriculumVitae.Application.Cv.Validators;
using FluentValidation;

namespace CurriculumVitae.Application.Certificate
{
    public class CertificateInputDtoValidator : AbstractValidator<CertificateInputDto>
    {
        public CertificateInputDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Institution).NotEmpty();
            RuleFor(x => x.Date).NotNull().LessThan(Clock.Now);
        }
    }
}