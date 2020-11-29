using System;
using Company.Application.Services.Location;
using Company.Application.Services.Sector;
using Company.Domain.Repositories;
using FluentValidation;

namespace Company.Application.Company.Commands.UpdateCompany
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommmand>
    {
        public UpdateCompanyCommandValidator(
            ISectorService sectorService,
            ILocationService locationService,
            ICompanyRepository companyRepository)
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
                .LessThanOrEqualTo((short)DateTime.UtcNow.Year);
                          
            RuleFor(x => x)
                .MustAsync(async (command, cancellation) => await locationService.IsCountryExistsAsync(command.Company.CountryId))
                .WithMessage("Country is not found!");

            RuleFor(x => x)
                .MustAsync(async (command, cancellation) => await locationService.IsCityExistsInCountryAsync(command.Company.CityId, command.Company.CountryId))
                .WithMessage("City is not found in country!");

            RuleFor(x => x)
                .MustAsync(async (command, cancellation) => await locationService.IsDistrictExistsInCityAsync(command.Company.DistrictId, command.Company.CityId))
                .WithMessage("District is not found in city!")
                .Unless(x => string.IsNullOrEmpty(x.Company.DistrictId));

            RuleFor(x => x)
                .MustAsync(async (command, cancellation) => await sectorService.IsSectorExistsAsync(command.Company.SectorId))
                .WithMessage("Company sector is not found!");

            RuleFor(x => x)
                .MustAsync(async (command, cancellation) => !await companyRepository.IsTaxNumberExistsAsync(command.Company.TaxNumber, command.Company.CountryId, command.Id))
                .WithMessage("Tax number is already registered!");
            
            //TODO: Tax Number validation
        }
    }
}