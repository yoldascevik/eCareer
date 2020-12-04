using System;
using FluentValidation;

namespace Company.Application.Company.Commands.UpdateCompany
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommmand>
    {
        public UpdateCompanyCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Company.CountryId).NotEmpty();
            RuleFor(x => x.Company.CityId).NotEmpty();
            RuleFor(x => x.Company.SectorId).NotEmpty();
            RuleFor(x => x.Company.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Company.Address).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Company.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Company.Phone).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Company.MobilePhone).MaximumLength(50);
            RuleFor(x => x.Company.Website).MaximumLength(50);
            RuleFor(x => x.Company.EmployeesCount).GreaterThan(0);
            RuleFor(x => x.Company.FaxNumber).MaximumLength(50);
            RuleFor(x => x.Company.TaxNumber).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Company.TaxOffice).NotEmpty().MaximumLength(50);

            RuleFor(x => x.Company.EstablishedYear)
                .GreaterThan((short) 1500)
                .LessThanOrEqualTo((short) DateTime.UtcNow.Year);

            //TODO: Tax Number validation
        }
    }
}