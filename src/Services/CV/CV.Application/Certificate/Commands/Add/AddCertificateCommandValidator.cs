using FluentValidation;

namespace CurriculumVitae.Application.Certificate.Commands.Add;

public class AddCertificateCommandValidator : AbstractValidator<AddCertificateCommand>
{
    public AddCertificateCommandValidator()
    {
        RuleFor(x => x.CvId).NotEmpty();
        RuleFor(x => x.Certificate).SetValidator(new CertificateInputDtoValidator());
    }
}