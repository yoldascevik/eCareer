using AutoMapper;
using Company.Application.Specifications.Company;
using FluentValidation;

namespace Company.Application.Commands.CreateCompany
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator(
            IMapper mapper,
            ICompanyLocationSpecification companyLocationSpecification,
            ICompanySectorSpecification companySectorSpecification,
            ICompanyTaxNumberSpecification companyTaxNumberSpecification)
        {
            RuleFor(x => x.CountryId).NotEmpty();
            RuleFor(x => x.CityId).NotEmpty();
            RuleFor(x => x.SectorId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Address).MaximumLength(500);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Phone).NotEmpty().MaximumLength(50);
            RuleFor(x => x.MobilePhone).MaximumLength(50);
            RuleFor(x => x.Website).MaximumLength(50);
            RuleFor(x => x.EmployeesCount).GreaterThan(0);
            RuleFor(x => x.EstablishedYear).GreaterThan((short) 0);
            RuleFor(x => x.FaxNumber).MaximumLength(50);
            RuleFor(x => x.TaxNumber).NotEmpty().MaximumLength(50);
            RuleFor(x => x.TaxOffice).NotEmpty().MaximumLength(50);
            
            RuleFor(x => x).MustAsync(async (command, cancellation) 
                => await companyLocationSpecification.IsSatisfiedByAsync(mapper.Map<Domain.Company>(command)));
            
            RuleFor(x => x).MustAsync(async (command, cancellation) 
                => await companySectorSpecification.IsSatisfiedByAsync(mapper.Map<Domain.Company>(command)));
            
            RuleFor(x => x).MustAsync(async (command, cancellation) 
                => await companyTaxNumberSpecification.IsSatisfiedByAsync(mapper.Map<Domain.Company>(command)));
        }
    }
}