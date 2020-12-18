using FluentValidation;

namespace Company.Application.Company.Commands.UpdateCompanyAddress
{
    public class UpdateCompanyAddressCommandValidator : AbstractValidator<UpdateCompanyAddressCommand>
    {
        public UpdateCompanyAddressCommandValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty();
            RuleFor(x => x.Address.CountryId).NotEmpty();
            RuleFor(x => x.Address.CityId).NotEmpty();
            RuleFor(x => x.Address.Address).NotEmpty().MaximumLength(500);
        }
    }
}