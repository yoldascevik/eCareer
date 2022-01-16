using FluentValidation;

namespace Company.Application.Company.Commands.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.Sector.RefId).NotEmpty();
        RuleFor(x => x.Sector).NotNull();
        RuleFor(x => x.Sector.Name).NotNull().NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(50);

        RuleFor(x => x.TaxInfo).NotNull()
            .ChildRules(c =>
            {
                c.RuleFor(x => x.TaxNumber).NotNull().NotEmpty().MaximumLength(50);
                c.RuleFor(x => x.TaxOffice).NotNull().NotEmpty().MaximumLength(50);
                c.RuleFor(x => x.TaxCountryId).NotNull().NotEmpty().MaximumLength(24);
            });
    }
}