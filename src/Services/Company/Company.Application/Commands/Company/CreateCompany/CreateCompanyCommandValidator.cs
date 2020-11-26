using Company.Application.Services.Abstractions;
using Company.Domain.Repository;
using FluentValidation;

namespace Company.Application.Commands.Company.CreateCompany
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator(
            ISectorService sectorService,
            ILocationService locationService,
            ICompanyRepository companyRepository)
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
            RuleFor(x => x.EstablishedYear).GreaterThan((short) 0);
            RuleFor(x => x.FaxNumber).MaximumLength(50);
            RuleFor(x => x.TaxNumber).NotEmpty().MaximumLength(50);
            RuleFor(x => x.TaxOffice).NotEmpty().MaximumLength(50);

            RuleFor(x => x)
                .MustAsync(async (command, cancellation) => await locationService.IsCountryExistsAsync(command.CountryId))
                .WithMessage("Country is not found!");

            RuleFor(x => x)
                .MustAsync(async (command, cancellation) => await locationService.IsCityExistsInCountryAsync(command.CityId, command.CountryId))
                .WithMessage("City is not found in country!");

            RuleFor(x => x)
                .MustAsync(async (command, cancellation) => await locationService.IsDistrictExistsInCityAsync(command.DistrictId, command.CityId))
                .WithMessage("District is not found in city!")
                .Unless(x => string.IsNullOrEmpty(x.DistrictId));

            RuleFor(x => x)
                .MustAsync(async (command, cancellation) => await sectorService.IsSectorExistsAsync(command.SectorId))
                .WithMessage("Company sector is not found!");

            RuleFor(x => x)
                .MustAsync(async (command, cancellation) => !await companyRepository.IsTaxNumberExistsAsync(command.TaxNumber, command.CountryId))
                .WithMessage("Tax number is already registered!");
            
            //TODO: Tax Number validation
        }
    }
}