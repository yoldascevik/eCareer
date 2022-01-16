using CurriculumVitae.Application.DrivingLicence.Dtos;
using FluentValidation;

namespace CurriculumVitae.Application.DrivingLicence;

public class DrivingLicenceInputDtoValidator : AbstractValidator<DrivingLicenceInputDto>
{
    public DrivingLicenceInputDtoValidator()
    {
        RuleFor(x => x.Class).NotEmpty();
        RuleFor(x => x.CertificateDate).NotEmpty().LessThan(x => x.ExpiredDate);
        RuleFor(x => x.ExpiredDate).GreaterThan(x => x.CertificateDate);
    }
}