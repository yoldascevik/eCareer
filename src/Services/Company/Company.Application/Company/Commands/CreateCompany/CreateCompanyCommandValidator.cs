using FluentValidation;

namespace Company.Application.Company.Commands.CreateCompany
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(x => x.CountryId).NotEmpty();
            RuleFor(x => x.CityId).NotEmpty();
            RuleFor(x => x.Sector.Id).NotEmpty();
            RuleFor(x => x.Sector).NotNull();
            RuleFor(x => x.Sector.Name).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Address).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Phone).NotEmpty().MaximumLength(50);
            RuleFor(x => x.TaxNumber).NotEmpty().MaximumLength(50);
            RuleFor(x => x.TaxOffice).NotEmpty().MaximumLength(50);
        }
    }
}