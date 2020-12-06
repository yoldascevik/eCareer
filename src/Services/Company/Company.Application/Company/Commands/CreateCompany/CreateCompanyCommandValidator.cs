using System;
using FluentValidation;

namespace Company.Application.Company.Commands.CreateCompany
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(x => x.CountryId).NotEmpty();
            RuleFor(x => x.CityId).NotEmpty();
            RuleFor(x => x.SectorId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Address).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Phone).NotEmpty().MaximumLength(50);
            RuleFor(x => x.MobilePhone).MaximumLength(50);
            RuleFor(x => x.Website).MaximumLength(50);
            RuleFor(x => x.EmployeesCount).GreaterThan(0);
            RuleFor(x => x.FaxNumber).MaximumLength(50);
            RuleFor(x => x.TaxNumber).NotEmpty().MaximumLength(50);
            RuleFor(x => x.TaxOffice).NotEmpty().MaximumLength(50);

            RuleFor(x => x.EstablishedYear)
                .GreaterThan((short) 1500)
                .LessThanOrEqualTo((short) DateTime.UtcNow.Year);
        }
    }
}