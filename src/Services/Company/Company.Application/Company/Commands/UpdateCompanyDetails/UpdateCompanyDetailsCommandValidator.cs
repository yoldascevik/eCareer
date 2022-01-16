using FluentValidation;

namespace Company.Application.Company.Commands.UpdateCompanyDetails;

public class UpdateCompanyDetailsCommandValidator : AbstractValidator<UpdateCompanyDetailsCommand>
{
    public UpdateCompanyDetailsCommandValidator()
    {
        RuleFor(x => x.Company.Sector.RefId).NotEmpty();
        RuleFor(x => x.Company.Sector.Name).NotNull().NotEmpty();
        RuleFor(x => x.Company.Phone).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Company.MobilePhone).MaximumLength(50);
        RuleFor(x => x.Company.Website).MaximumLength(50);
        RuleFor(x => x.Company.EmployeesCount).GreaterThan(0);
        RuleFor(x => x.Company.FaxNumber).MaximumLength(50);

        RuleFor(x => x.Company.EstablishedYear)
            .GreaterThan((short) 1500)
            .LessThanOrEqualTo((short) DateTime.UtcNow.Year);
    }
}