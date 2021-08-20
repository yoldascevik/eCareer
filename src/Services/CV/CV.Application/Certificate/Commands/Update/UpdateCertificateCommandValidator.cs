using FluentValidation;

namespace CurriculumVitae.Application.Certificate.Commands.Update
{
    public class UpdateCertificateCommandValidator : AbstractValidator<UpdateCertificateCommand>
    {
        public UpdateCertificateCommandValidator()
        {
            RuleFor(x => x.CvId).NotEmpty();
            RuleFor(x => x.CertificateId).NotEmpty();
            RuleFor(x => x.Certificate).SetValidator(new CertificateInputDtoValidator());
        }
    }
}