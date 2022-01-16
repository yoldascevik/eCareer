using FluentValidation;

namespace Company.Application.Company.Commands.UpdateCompanyTaxInfo;

public class UpdateCompanyTaxInfoCommandValidator : AbstractValidator<UpdateCompanyTaxInfoCommand>
{
    public UpdateCompanyTaxInfoCommandValidator()
    {
        RuleFor(x => x.CompanyId).NotEmpty();
        RuleFor(x => x.TaxInfo.TaxCountryId).NotEmpty();
        RuleFor(x => x.TaxInfo.TaxNumber).NotEmpty().MaximumLength(50);
        RuleFor(x => x.TaxInfo.TaxOffice).NotEmpty().MaximumLength(50);
    }
}